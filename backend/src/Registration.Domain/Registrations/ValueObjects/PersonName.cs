using System.Text.RegularExpressions;
using Registration.Domain.Common;

namespace Registration.Domain.Registrations.ValueObjects;

/// <summary>
/// A person's name (first, optional middle, last). Each part is trimmed, has its
/// internal whitespace collapsed, and may only contain Arabic or English letters
/// plus space, hyphen and apostrophe separators.
/// </summary>
public sealed partial class PersonName : ValueObject
{
    public const int MaxPartLength = 50;

    // One or more Arabic/English letters, optionally joined by a single space,
    // hyphen or apostrophe (e.g. "Al-Sayed", "O'Brien", Arabic names).
    // ؀-ۿ is the Arabic Unicode block; \u escapes keep the source ASCII.
    [GeneratedRegex(@"^[A-Za-z؀-ۿ]+(?:[ '\-][A-Za-z؀-ۿ]+)*$",
        RegexOptions.CultureInvariant)]
    private static partial Regex AllowedPattern();

    [GeneratedRegex(@"\s+", RegexOptions.CultureInvariant)]
    private static partial Regex WhitespaceRuns();

    private PersonName(string first, string? middle, string last)
    {
        First = first;
        Middle = middle;
        Last = last;
    }

    public string First { get; }

    public string? Middle { get; }

    public string Last { get; }

    public string FullName => Middle is null ? $"{First} {Last}" : $"{First} {Middle} {Last}";

    public static PersonName Create(string? first, string? middle, string? last)
    {
        var firstPart = ValidatePart(first, "First name", required: true)!;
        var middlePart = ValidatePart(middle, "Middle name", required: false);
        var lastPart = ValidatePart(last, "Last name", required: true)!;
        return new PersonName(firstPart, middlePart, lastPart);
    }

    /// <summary>Trims and collapses repeated internal whitespace to a single space.</summary>
    public static string Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? string.Empty : WhitespaceRuns().Replace(value.Trim(), " ");

    public static bool IsValidPart(string? value)
    {
        var normalized = Normalize(value);
        return normalized.Length > 0
               && normalized.Length <= MaxPartLength
               && AllowedPattern().IsMatch(normalized);
    }

    private static string? ValidatePart(string? value, string field, bool required)
    {
        var normalized = Normalize(value);
        if (normalized.Length == 0)
        {
            if (required)
            {
                throw new DomainException($"{field} is required.");
            }

            return null;
        }

        if (normalized.Length > MaxPartLength)
        {
            throw new DomainException($"{field} must not exceed {MaxPartLength} characters.");
        }

        if (!AllowedPattern().IsMatch(normalized))
        {
            throw new DomainException(
                $"{field} may only contain Arabic or English letters, spaces, hyphens and apostrophes.");
        }

        return normalized;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return First;
        yield return Middle;
        yield return Last;
    }
}
