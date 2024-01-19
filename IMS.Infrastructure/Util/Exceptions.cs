using System.Resources;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Localization;

namespace IMS.Infrastructure.Util;
/// <summary>
/// parent custom exception class for all custom exception made for this application.
/// this can be helpful to only filter custom exception in try catch statement
/// </summary>
/// <param name="message"></param>
public class CustomException(string message) : Exception(message);
public class RecordNotFoundException(string message) : CustomException(message);

public class EmailOrPasswordNotValidException(string message) : CustomException(message);
public class ActionNotValidException(string? message): CustomException(message);
public class UserNotAuthenticatedException(string? message): CustomException(message);
public class UserNotAuthorizedException(string? message): CustomException(message);