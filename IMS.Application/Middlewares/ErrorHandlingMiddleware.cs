using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IMS.Infrastructure.Util;

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
        else if (exception is ActionNotValidException) code = HttpStatusCode.MethodNotAllowed;

        var result = JsonConvert.SerializeObject(new {  = exception.Message });
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}