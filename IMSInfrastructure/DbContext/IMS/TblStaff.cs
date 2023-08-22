using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMSInfrastructure.DbContext.IMS;

[Index(nameof(StaffId), IsUnique = true)]
public class TblStaff:TblBase
{
    [Column(TypeName = "varchar(200)")]
    public string Name { get; set; }
    [Column(TypeName = "varchar(10)")]
    public string StaffId { get; set; } 
    [Column(TypeName = "user_gender")]
    public UserGender Gender { get; set; }
    public Guid UserId { get; set; }
    public TblUser User { get; set; }
}