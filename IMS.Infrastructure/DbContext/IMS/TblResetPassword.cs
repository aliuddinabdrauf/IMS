using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblResetPassword : TblBase
{
   //only valid for 24 hour after send
   [Required]
   public Duration Validity { get; set; } = Duration.FromHours(24);
   [Required]
   [StringLength(maximumLength:100)]
   public string ResetKey { get; set; } = null!;
   public Instant? TimestampSend { get; set; }
   [Required]
   public bool IsUsed { get; set; }
   [Required]
   public bool IsFirstTimeUser { get; set; }
   [Required]
   public Guid UserId { get; set; }
   public virtual TblUser User { get; set; } = null!;
}