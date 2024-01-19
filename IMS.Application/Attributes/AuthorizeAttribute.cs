using IMS.Infrastructure;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace IMS.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(params UserRole[] roles) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var localeService = context.HttpContext.RequestServices.GetService(typeof(IStringLocalizer<GlobalResource>)) as IStringLocalizer<GlobalResource>;
        var user = (UserDto?)context.HttpContext.Items["User"];
        if (user == null)
        {
            throw new UserNotAuthenticatedException(localeService?["NotAuthenticated"] ?? "Not Authenticated");
        }
        
        if (roles.Length > 0 && roles.Any(r => user.Roles.Any(ro => ro == r)))
        {
            throw new UserNotAuthorizedException(localeService?["NotAuthorized"] ?? "Not Authorized");
        }
    }
}