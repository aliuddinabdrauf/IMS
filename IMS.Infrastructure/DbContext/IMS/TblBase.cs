using System.ComponentModel.DataAnnotations;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace IMS.Infrastructure.DbContext.IMS;

public partial class TblBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Instant TimestampCreated { get; init; } = SystemClock.Instance.GetCurrentInstant();
    public Instant TimestampUpdated { get; init; } = SystemClock.Instance.GetCurrentInstant();
}