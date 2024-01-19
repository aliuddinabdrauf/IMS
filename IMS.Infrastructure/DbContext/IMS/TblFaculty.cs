using System.ComponentModel.DataAnnotations;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblFaculty:TblBase
{
    [Required]
    [StringLength(10)]
    public string Code { get; set; } = null!;
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    [StringLength(200)]
    public string? Description { get; set; }
    public virtual ICollection<TblCourse> Courses { get; } = [];
}