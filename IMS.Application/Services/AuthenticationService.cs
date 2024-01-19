using System.Security.Cryptography;
using IMS.Application.UoW;
using IMS.Infrastructure;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using NodaTime;

namespace IMS.Application.Services;

public interface IAuthenticationServices
{
    string GenerateSaltAndPasswordHash(string userId, string password, out string salt);
    void VerifyPassword(string userId, string password, string salt, string hashedPassword);
    Task<ResetPasswordDto> GenerateResetPassword(Guid userId, bool isFirstTimeUser, bool autoSave = true);
}

public class AuthenticationService(IStringLocalizer<GlobalResource> globalResource, IUnitOfWork unitOfWork) : IAuthenticationServices
{
    private readonly IStringLocalizer<GlobalResource> _stringLocalizer = globalResource;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
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
        string url = $"http://dummyurl/resetpassword/{userId}/{key}"; //TODO: create a correct url by referencing to the appsetting file
        var dto = new ResetPasswordDto(null,userId, key, url, isFirstTimeUser);
        var result =await _unitOfWork.AuthenticationRepositories.CreateResetPassword(dto);
        if (autoSave) await _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task ValidateResetPasswordRequest(Guid resetId, string resetKey)
    {
        var data = await _unitOfWork.AuthenticationRepositories.GetResetPasswordDetailsToValidate(resetId, resetKey);
        if (data.TimestampSend.Plus(data.Validity) > SystemClock.Instance.GetCurrentInstant())
        {
            throw new InvalidOperationException("");
        }
    }
}