using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMSInfrastructure.DbContext.IMS;

[Index(nameof(Email), IsUnique = true)]
public class TblUser:TblBase
{
    [Column(TypeName = "varchar(200)")]
    public string Email { get; set; }
    [Column(TypeName = "Text")]
    public string PasswordHash { get; set; }
    [Column(TypeName = "Text")]
    public string PasswordSalt { get; set; }
    [Column(TypeName = "varchar(12)")]
    public string? PhoneNo { get; set; }
    [Column(TypeName = "user_role[]")]
    public UserRole[] Roles { get; set; } = Array.Empty<UserRole>();
    [Column(TypeName = "user_status")]
    public UserStatus Status { get; set; }
    [Column(TypeName = "user_type")]
    public UserType Type { get; set; }
    public TblStudent Student { get; set; }
    public TblStaff Staff { get; set; }
}
