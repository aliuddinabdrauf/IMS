using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IMS.Application.Repositories;
using IMS.Application.Services;
using IMS.Infrastructure.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IMS.Application.Middlewares;

//refer to https://www.c-sharpcorner.com/article/authentication-and-authorization-in-net-8-web-api/
public class JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
{
    private readonly RequestDelegate _next = next;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task Invoke(HttpContext context, IAuthenticationService authenticationService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await AttachUserToContext(context, authenticationService, token);

        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IAuthenticationService authenticationService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var sessionId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "sessionId").Value);
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

            //Attach user to context on successful JWT validation
            context.Items["User"] = await authenticationService.ValidateLoginSession(userId, sessionId);
        }
        catch
        {
            //Do nothing if JWT validation fails
            // user is not attached to context so the request won't have access to secure routes
        }
    }
}