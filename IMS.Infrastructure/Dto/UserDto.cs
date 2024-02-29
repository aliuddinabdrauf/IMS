using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using IMS.Infrastructure.DbContext.IMS;

namespace IMS.Infrastructure.Dto;

#region dto

public class UserDto{
    public Guid? Id { get; set; }
    public Guid? ByAccountTypeId { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string Name {get ;set;} = null!;
    public UserRole[] Roles { get; set; } = null!;
    public UserStatus Status { get; set; }
    public AccountType Type { get; set; }
    public Guid? ProfilePicture {get ;set;}
    [JsonIgnore]
    public string? PasswordHash { get; set; }
    [JsonIgnore]
    public string? PasswordSalt { get; set; }
}
public class UserAfterLoginDto : UserDto
{
    public string Name { get; set; } = null!;
}
public record CreateUserAsStudentDto(
    [param:Required][param:StringLength(maximumLength:100, MinimumLength = 1)]string Name,
    [param:EmailAddress][param:StringLength(maximumLength:100)]string EmailAddress, 
    [param:Required][param:StringLength(maximumLength:10, MinimumLength = 10)]string StudentId,
    [param:Required][param:StringLength(maximumLength:12, MinimumLength = 12)]string IcNo, 
    [param:Required]UserGender Gender, 
    [param:Length(minimumLength:1, maximumLength:3)]Guid[] CoursesIds);

public record ResetUserPasswordDto(Guid Id, string PasswordHash, string PasswordSalt);
#endregion
