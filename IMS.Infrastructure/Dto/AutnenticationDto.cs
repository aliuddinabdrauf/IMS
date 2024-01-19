using System.ComponentModel.DataAnnotations;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Attributes;
using Microsoft.Extensions.Configuration;
using NodaTime;

namespace IMS.Infrastructure.Dto;

public record ResetPasswordDto(Guid? Id, Guid UserId, string ResetKey, bool IsFirstTimeUser);

public record ResetPasswordToValidateDto(Duration Validity, Instant TimestampSend, bool IsUsed, Guid UserId);

public class ResetPasswordFromClientDto
{
    [Required]
    public Guid ResetId { get; init; }
    [Required]
    public string ResetKey { get; init; } = null!;
    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 8)]
    [PasswordIsValid]
    public string NewPassword { get; init; } = null!;
    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 8)]
    [Compare("NewPassword")]
    [PasswordIsValid]
    public string ConfirmNewPassword { get; init; } = null!;
}
public record LoginDto(
    AccountType AccountType,
    string EmailAddress,
    string Password
    );
    
public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    /// <summary>
    /// duration of validity of token in days
    /// </summary>
    public int TokenValidity { get; set; }

    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Subject { get; set; } = null!;
    [ConfigurationKeyName("Claims:UserId")]
    public string ClaimUserId { get; set; } = null!;
    [ConfigurationKeyName("Claims:SessionId")]
    public string ClaimSessionId{ get; set; } = null!;

}

public class LoginSessionDto
{
    public Guid? Id { get; set; }
    public Guid UserId { get; set; }
    public Instant? TimestampStart { get; set; }
    public Instant? TimestampEnd { get; set; }
    public Instant TimestampLatestActivity { get; set; }
    public bool IsRejected { get; set; }
}
public record SuccessLoginDto(string Token, UserDto User);