using System.Net;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMS.Application.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke (HttpContext context)
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
        
        var error = new ResponseProblemDto{Status = code.GetHashCode(), Type = exception.GetType().Name, Title = exception.Message, Details = exception.Message};

        if (exception.IsSystemException())
        {
            error.Title = "Something wrong happened";
            error.Details = "No Details";
        }
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsJsonAsync(error);
    }
}