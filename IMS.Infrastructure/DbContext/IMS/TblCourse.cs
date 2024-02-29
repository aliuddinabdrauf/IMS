using System.ComponentModel.DataAnnotations;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblCourse:TblBase
{
    [Required]
    [StringLength(10)]
    public string Code { get; set; } = null!;
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;
    [StringLength(300)]
    public string? Description { get; set; }
    public Guid FacultyId { get; set; }
    public virtual TblFaculty Faculty { get; set; } = null!;
    public virtual ICollection<TblStudentCourse> StudentCourses { get; set; } = [];

}