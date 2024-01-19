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
[Route("api/public")]
public class PublicController(IStringLocalizer<GlobalResource> localizer, IAuthenticationService authenticationService): ControllerBase
{
    
}