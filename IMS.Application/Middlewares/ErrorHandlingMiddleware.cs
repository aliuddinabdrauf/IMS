using System.Net;
using IMS.Infrastructure.Util;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IMS.Application.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        if (exception is RecordNotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is ActionNotValidException or EmailOrPasswordNotValidException) code = HttpStatusCode.BadRequest;
        else if (exception is UserNotAuthorizedException) code = HttpStatusCode.Forbidden;
        else if (exception is UserNotAuthenticatedException) code = HttpStatusCode.Unauthorized;

        string result = "message: something wrong happened!";
        if (!exception.IsSystemException())
        {
            result  = JsonConvert.SerializeObject(new { message = exception.Message });
        }
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}