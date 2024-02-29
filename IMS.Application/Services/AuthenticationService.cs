using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IMS.Application.UoW;
using IMS.Infrastructure;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using NodaTime.Extensions;
using UserNotAuthenticatedException = IMS.Infrastructure.Util.UserNotAuthenticatedException;

namespace IMS.Application.Services;

public interface IAuthenticationService
{
    string GenerateSaltAndPasswordHash(string userId, string password, out string salt);
    void VerifyPassword(string userId, string password, string salt, string hashedPassword);
    Task<ResetPasswordDto> GenerateResetPassword(Guid userId, bool isFirstTimeUser, bool autoSave = true);
    Task ResetPasswordOnRequest(string emailAddress);
    Task<ResetPasswordToValidateDto> ValidateResetPasswordRequest(Guid resetId, string resetKey);
    Task ResetPassword(ResetPasswordFromClientDto reset);
    Task<UserDto> Login(LoginDto login);
    Task<string> GenerateJwtToken(UserDto user);
    Task<UserDto> ValidateLoginSession(Guid userId, Guid sessionId);
}

public class AuthenticationService(IStringLocalizer<GlobalResource> globalResource, IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings) : IAuthenticationService
{
    private readonly IStringLocalizer<GlobalResource> _stringLocalizer = globalResource;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    public string GenerateSaltAndPasswordHash(string userId, string password, out string salt)
    {
        salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return new PasswordHasher<string>().HashPassword(userId, password + salt);
    }

    public void VerifyPassword(string userId, string password, string salt, string hashedPassword)
    {
        var result = new PasswordHasher<string>().VerifyHashedPassword(userId, hashedPassword, password + salt);
        switch (result)
        {
            case PasswordVerificationResult.Failed:
                throw new EmailOrPasswordNotValidException(_stringLocalizer["EmailOrPasswordNotValid"]);
            case PasswordVerificationResult.SuccessRehashNeeded:
                //TODO: call rehashing services here, but will not block the user from login if the service failed to rehash
                break;
            case PasswordVerificationResult.Success:
                return;
            default:
                throw new ArgumentOutOfRangeException("",message:_stringLocalizer["NoValidAction"]);
        }
    }
    public async Task<ResetPasswordDto> GenerateResetPassword(Guid userId, bool isFirstTimeUser, bool autoSave = true)
    {
        string key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var dto = new ResetPasswordDto(null,userId, key, isFirstTimeUser);
        var result =await _unitOfWork.AuthenticationRepository.CreateResetPassword(dto);
        if (autoSave) await _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task ResetPasswordOnRequest(string emailAddress)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmailAddress(emailAddress);
        if (user.Id.HasValue)
            await GenerateResetPassword(user.Id.Value, false);
        else
            throw new NullReferenceException();
    }

    public async Task<ResetPasswordToValidateDto> ValidateResetPasswordRequest(Guid resetId, string resetKey)
    {
        var data = await _unitOfWork.AuthenticationRepository.GetResetPasswordDetailsToValidate(resetId, resetKey);
        if (data.TimestampSend.Plus(data.Validity) < SystemClock.Instance.GetCurrentInstant() || data.IsUsed)
        {
            throw new ActionNotValidException("");
        }

        return data;
    }
    public async Task ResetPassword(ResetPasswordFromClientDto reset)
    {
        var data = await ValidateResetPasswordRequest(reset.ResetId, reset.ResetKey);
        var hashedPassword = GenerateSaltAndPasswordHash(data.UserId.ToString(), reset.NewPassword, out var salt);
        _unitOfWork.UserRepository.UpdatePasswordHashAndSalt(new ResetUserPasswordDto(data.UserId, hashedPassword, salt));
        _unitOfWork.AuthenticationRepository.ResetPasswordMarkAsUsed(reset.ResetId);
        await _unitOfWork.CompleteAsync();
    }
    public async Task<UserDto> Login(LoginDto login)
    {
        var user = await _unitOfWork.UserRepository.GetUserForLogin(login);
        VerifyPassword(user.Id.ToString()!, login.Password, user.PasswordSalt!, user.PasswordHash!);
        return user;
    }

    private async Task<LoginSessionDto> CreateLoginSession(Guid userId,SecurityTokenDescriptor token)
    {
        var loginSession = new LoginSessionDto()
        {
            UserId = userId,
            TimestampStart = SystemClock.Instance.GetCurrentInstant(),
            TimestampEnd = token.Expires.GetValueOrDefault().ToInstant(),
            TimestampLatestActivity = SystemClock.Instance.GetCurrentInstant()
        };
        var result = await _unitOfWork.AuthenticationRepository.CreateUserSession(loginSession);
        await _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<string> GenerateJwtToken(UserDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var claims = new[] { new Claim("id", user.Id.ToString()!) };
        var token = await Task.Run(async () =>
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", user.Id.ToString()!), 
                new Claim("accountType", user.Type.GetHashCode().ToString(), ClaimValueTypes.Integer),
                new Claim("emailAddress", user.EmailAddress),
                new Claim("name", user.Name) }),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.TokenValidity),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            foreach (var role in user.Roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(type:"userRole", role.GetHashCode().ToString(), ClaimValueTypes.Integer));
            }
            var sessionId = await CreateLoginSession(user.Id.GetValueOrDefault(), tokenDescriptor);
            tokenDescriptor.Subject.AddClaim(new Claim(type:"sessionId", sessionId.Id.ToString()!));
            return tokenHandler.CreateToken(tokenDescriptor);
        });
        return tokenHandler.WriteToken(token);
    }

    public async Task<UserDto> ValidateLoginSession(Guid userId, Guid sessionId)
    {
        var result = await _unitOfWork.UserRepository.GetUserActiveById(userId);
        var sessionUpdated = await _unitOfWork.AuthenticationRepository.UpdateLoginSession_E(sessionId);
        if (sessionUpdated == 0)
            throw new UserNotAuthenticatedException("");
        return result;
    }
}