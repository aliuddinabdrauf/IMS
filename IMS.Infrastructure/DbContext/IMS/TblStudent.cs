using System.ComponentModel.DataAnnotations;
using IMS.Infrastructure.Attributes;

namespace IMS.Infrastructure.DbContext.IMS;
public partial class TblStudent:TblBase
{
    [StringLength(200)]
    [Required]
    public string Name { get; set; } = null!;
    [StringLength(maximumLength:10, MinimumLength =10)]
    [Required]
    public string StudentId { get; set; } = null!;
    [StringLength(maximumLength:12, MinimumLength =12)]
    [Required]
    public string IcNo { get; set; } = null!;
    [Length(minimumLength:0, maximumLength:3)]
    [StringArrayLength(MinimumLength = 9, MaximumLength = 12)]
    public List<string> PhoneNo { get; set; } = [];
    [Length(minimumLength:0, maximumLength:3)]
    [StringArrayLength(MinimumLength = 1, MaximumLength = 300)]
    public List<string> Address { get; set; } = [];
    [Required]
    public UserGender Gender { get; set; }
    [Length(minimumLength:0, maximumLength:100000)]
    public byte[]? ProfilePicture { get; set; }
    public Guid UserId { get; set; }
    public TblUser User { get; set; } = null!;
    public virtual ICollection<TblStudentCourse> StudentCourses { get; } = [];
}