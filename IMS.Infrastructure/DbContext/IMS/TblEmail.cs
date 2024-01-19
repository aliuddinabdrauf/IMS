using System.ComponentModel.DataAnnotations;
using IMS.Infrastructure.Attributes;
using NodaTime;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblEmail : TblBase
{
    public Instant TimestampSend { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Sender { get; set; } = null!;
    [ArrayOnlyHaveEmailAddress]
    public string[] To { get; set; } = [];
    [ArrayOnlyHaveEmailAddress]
    public string[] Cc { get; set; } = [];
    [ArrayOnlyHaveEmailAddress]
    public string[] Bcc { get; set; } = [];
    [StringLength(100)]
    public string? Subject { get; set; }
    [StringLength(int.MaxValue)]
    public string Body { get; set; } = null!;
    [StringLength(30)]
    public string? Reference { get; set; }
    public Guid? ReferenceId { get; set; }
}