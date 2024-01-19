using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblLoginSession: TblBase
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Instant TimestampStart { get; set; }
    [Required]
    public Instant TimestampEnd { get; set; }
    [Required]
    public Instant TimestampLatestActivity { get; set; }
    public bool IsRejected { get; set; }
    public virtual TblUser User { get; set; } = null!;
}