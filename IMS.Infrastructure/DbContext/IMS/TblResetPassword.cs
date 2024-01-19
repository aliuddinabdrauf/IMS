using NodaTime;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblResetPassword : TblBase
{
   //only valid for 24 hour after send
   public Duration Validity { get; set; } = Duration.FromHours(24);
   public string ConfirmKey { get; set; } = null!;
   public string ConfirmUrl { get; set; } = null!;
   public Instant TimestampSend { get; set; } = SystemClock.Instance.GetCurrentInstant();
   public Guid UserId { get; set; }
   public virtual TblUser User { get; set; } = null!;
}