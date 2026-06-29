using Registration.Domain.Common;

namespace Registration.Domain.Registrations.ValueObjects;

/// <summary>
/// A date of birth (date only). It cannot be in the future and the registrant
/// must be at least <see cref="MinimumAgeYears"/> years old on the submission
/// date. Age is calculated accurately (accounting for whether the birthday has
/// already occurred this year), not by a naive year subtraction.
/// </summary>
public sealed class BirthDate : ValueObject
{
    public const int MinimumAgeYears = 20;

    private BirthDate(DateOnly value) => Value = value;

    public DateOnly Value { get; }

    /// <param name="value">The date of birth.</param>
    /// <param name="today">The submission date (injected for deterministic rules).</param>
    public static BirthDate Create(DateOnly value, DateOnly today)
    {
        if (value > today)
        {
            throw new DomainException("Birth date cannot be in the future.");
        }

        if (CalculateAge(value, today) < MinimumAgeYears)
        {
            throw new DomainException($"Registrant must be at least {MinimumAgeYears} years old.");
        }

        return new BirthDate(value);
    }

    public static bool IsEligible(DateOnly value, DateOnly today) =>
        value <= today && CalculateAge(value, today) >= MinimumAgeYears;

    public int AgeOn(DateOnly asOf) => CalculateAge(Value, asOf);

    private static int CalculateAge(DateOnly birthDate, DateOnly asOf)
    {
        var age = asOf.Year - birthDate.Year;

        // Subtract a year if the birthday has not yet occurred in the "asOf" year.
        if (birthDate > asOf.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString("yyyy-MM-dd");
}
