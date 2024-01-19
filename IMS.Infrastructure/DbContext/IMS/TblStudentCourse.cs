namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblStudentCourse : TblBase
{
    public Guid StudentId { get; set; }

    public Guid CourseId { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual TblStudent Student { get; set; } = null!;
    public virtual TblCourse Course { get; set; } = null!;
}