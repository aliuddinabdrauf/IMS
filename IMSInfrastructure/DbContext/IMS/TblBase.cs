using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace IMSInfrastructure.DbContext.IMS;

public class TblBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Instant CreateTime { get; set; } = SystemClock.Instance.GetCurrentInstant();
    public Instant UpdateTime { get; set; } = SystemClock.Instance.GetCurrentInstant();
}