using System.Text.RegularExpressions;
using Registration.Domain.Common;

namespace Registration.Domain.Registrations.ValueObjects;

/// <summary>
/// An email address. Keeps the original (trimmed) value for display and a
/// lower-invariant <see cref="Normalized"/> form used for case-insensitive
/// uniqueness checks and indexing.
/// </summary>
public sealed partial class Email : ValueObject
{
    public const int MaxLength = 254;

    // Pragmatic single-line check: non-space/@ local part, "@", domain with a dot.
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.CultureInvariant)]
    private static partial Regex Pattern();

    private Email(string value, string normalized)
    {
        Value = value;
        Normalized = normalized;
    }

    /// <summary>The trimmed value as supplied by the user.</summary>
    public string Value { get; }

    /// <summary>Lower-invariant form for uniqueness comparison and indexing.</summary>
    public string Normalized { get; }

    public static Email Create(string? value)
    {
        var trimmed = value?.Trim() ?? string.Empty;

        if (trimmed.Length == 0)
        {
            throw new DomainException("Email is required.");
        }

        if (trimmed.Length > MaxLength)
        {
            throw new DomainException($"Email must not exceed {MaxLength} characters.");
        }

        if (!Pattern().IsMatch(trimmed))
        {
            throw new DomainException("Email format is invalid.");
        }

        return new Email(trimmed, trimmed.ToLowerInvariant());
    }

    public static bool IsValid(string? value)
    {
        var trimmed = value?.Trim() ?? string.Empty;
        return trimmed.Length is > 0 and <= MaxLength && Pattern().IsMatch(trimmed);
    }

    /// <summary>Normalized (lower-invariant, trimmed) form, or empty when null/blank.</summary>
    public static string NormalizeOrEmpty(string? value) =>
        string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Normalized;
    }

    public override string ToString() => Value;
}
