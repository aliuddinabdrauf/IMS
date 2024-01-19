using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.DbContext.IMS;
[Index(nameof(StudentId), IsUnique = true)]
public class TblStudent:TblBase
{
    public string Name { get; set; }
    [Column(TypeName = "varchar(10)"), StringLength(10)]
    public string StudentId { get; set; }
    [Column(TypeName = "user_gender")]
    public UserGender Gender { get; set; }
    public Guid UserId { get; set; }
    public TblUser User { get; set; }
}