using Registration.Application.Common.Interfaces;

namespace Registration.Infrastructure.Time;

/// <summary>The real system clock (UTC).</summary>
public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
}
