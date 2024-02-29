using System.ComponentModel.DataAnnotations;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblFile: TblBase
{
    [Length(maximumLength:1000000000, minimumLength:1)]
    public byte[] File { get; set; } = null!;
    public ICollection<TblFileDetail> Previews {get; set;} = [];
    public ICollection<TblFileDetail> Actuals {get; set;} = [];
}
