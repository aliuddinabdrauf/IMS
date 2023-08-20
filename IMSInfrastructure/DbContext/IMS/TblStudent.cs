using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace IMSInfrastructure.DbContext.IMS;

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