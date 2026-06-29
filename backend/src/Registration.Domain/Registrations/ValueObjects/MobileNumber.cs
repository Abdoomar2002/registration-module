using System.Text.RegularExpressions;
using Registration.Domain.Common;

namespace Registration.Domain.Registrations.ValueObjects;

/// <summary>
/// A mobile number stored in normalized E.164 form (e.g. +201006158123).
/// Common separators (spaces, hyphens, parentheses) are stripped on creation so
/// values can be compared and uniquely indexed reliably.
/// </summary>
public sealed partial class MobileNumber : ValueObject
{
    // E.164: a leading '+', a non-zero first digit, then a total of 8..15 digits.
    [GeneratedRegex(@"^\+[1-9]\d{7,14}$", RegexOptions.CultureInvariant)]
    private static partial Regex Pattern();

    [GeneratedRegex(@"[\s\-()]", RegexOptions.CultureInvariant)]
    private static partial Regex SeparatorsToStrip();

    private MobileNumber(string value) => Value = value;

    /// <summary>The normalized E.164 value.</summary>
    public string Value { get; }

    public static MobileNumber Create(string? value)
    {
        var normalized = Normalize(value);

        if (normalized.Length == 0)
        {
            throw new DomainException("Mobile number is required.");
        }

        if (!Pattern().IsMatch(normalized))
        {
            throw new DomainException(
                "Mobile number must be a valid E.164 number, for example +201006158123.");
        }

        return new MobileNumber(normalized);
    }

    public static bool IsValid(string? value) => Pattern().IsMatch(Normalize(value));

    /// <summary>Strips common separators and trims; does not validate.</summary>
    public static string Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? string.Empty : SeparatorsToStrip().Replace(value.Trim(), string.Empty);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
