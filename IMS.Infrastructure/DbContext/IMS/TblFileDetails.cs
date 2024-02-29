using System.ComponentModel.DataAnnotations;
using IMS.Infrastructure.DbContext.IMS;

namespace IMS.Infrastructure;

public class TblFileDetail: TblBase
{
    [Required]
    [StringLength(50)]
    public string Name {get ;set;} = null!;
    public FileType Type {get ;set;}
    public bool IsExternal {get;set;}
    public Guid? Preview {get ;set;}
    public Guid Actual {get; set;}
    public TblFile? FilePreview {get ;set;}
    public TblFile FileActual {get ;set;} = null!;
    public virtual TblUser User {get ;set;}
}
