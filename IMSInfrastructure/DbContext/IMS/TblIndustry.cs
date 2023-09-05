using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMSInfrastructure.DbContext.IMS;

public class TblIndustry:TblBase
{
    [Column(TypeName = "varchar(200)")]
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public TblUser User { get; set; }
}
