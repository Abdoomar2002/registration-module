namespace Registration.Application.Common.Interfaces;

/// <summary>
/// Abstraction over the system clock so time-dependent rules (age, audit
/// timestamps) stay deterministic and unit-testable.
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateOnly Today { get; }
}
