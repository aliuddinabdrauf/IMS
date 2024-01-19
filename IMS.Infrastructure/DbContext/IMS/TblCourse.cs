namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblFaculty:TblBase
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
}