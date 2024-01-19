using System.ComponentModel.DataAnnotations;
using IMS.Application.Services;
using IMS.Infrastructure;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IMS.WebApi.Controllers;
[ApiController]
[AllowAnonymous]
[Route("api/authentication")]
public class AuthenticateController(IAuthenticationService authenticationService, IStringLocalizer<GlobalResource> localizer, IUserService userService) : ControllerBase
{
    private readonly IStringLocalizer<GlobalResource> _localizer = localizer;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IUserService _userService = userService;
    [HttpGet]
    [Route("resetpassword/request")]
    public async Task<IActionResult> ResetPasswordRequest([EmailAddress]string emailAddress)
    {
        //even if email not found, just return success
        try
        {
            await _authenticationService.ResetPasswordOnRequest(emailAddress);
        }
        catch (Exception e)
        {
            //if the exception is custom, then do not re throw the error
            if (!e.IsCustomException())
                throw;
        }
        return Accepted(new ResponseDto(Message:_localizer["RequestResetPassword"]));
    }

    [HttpGet]
    [Route("resetpassword/validate")]
    public async Task<IActionResult> ResetPasswordValidate(Guid resetId, string resetKey)
    {
        await _authenticationService.ValidateResetPasswordRequest(resetId, resetKey);
        return NoContent();
    }
    
    [HttpPost]
    [Route("resetpassword/reset")]
    public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordFromClientDto reset)
    {
        await _authenticationService.ResetPassword(reset);
        return NoContent();
    }
    [HttpGet]
    [Route("trygeneratehash")]
    public IActionResult TryGenerateHash(Guid userId, string password)
    {
        var hash = _authenticationService.GenerateSaltAndPasswordHash(userId.ToString(), password, out string salt);
        return Ok(hash + "   "+ salt);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        try
        {
            var user = await _authenticationService.Login(login);
            await _userService.RetrieveByAccountTypeId(user);
            var jwtToken = await _authenticationService.GenerateJwtToken(user);
            return Ok(new SuccessLoginDto(jwtToken, user));
        }
        catch (RecordNotFoundException e)
        {
            throw new ActionNotValidException(_localizer["EmailOrPasswordNotValid"]);
        }
    }
}