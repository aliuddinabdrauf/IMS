using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.DbContext.IMS;

public class TblStaff:TblBase
{
    public string Name { get; set; }
    public string StaffId { get; set; } 
    public UserGender Gender { get; set; }
    public Guid UserId { get; set; }
    public TblUser User { get; set; } = null!;

}