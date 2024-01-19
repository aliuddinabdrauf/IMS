using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblUser:TblBase
{
    private string _emailAddress = null!;
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string EmailAddress {
        get => _emailAddress;
        set => _emailAddress = value.ToLowerInvariant();
    }
    [StringLength(200)]
    public string? PasswordHash { get; set; }
    [StringLength(100)]
    public string? PasswordSalt { get; set; }
    public UserRole[] Roles { get; set; } = [];
    public UserStatus Status { get; set; }
    public AccountType Type { get; set; }
    public TblStudent? Student { get; set; }
    public TblStaff? Staff { get; set; }
    public virtual ICollection<TblResetPassword> ResetPasswords { get; set; } = [];
    public virtual ICollection<TblLoginSession> LoginSessions { get; set; } = [];
}
