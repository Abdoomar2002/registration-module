using System.Text.RegularExpressions;
using Registration.Domain.Common;

namespace Registration.Domain.Registrations;

/// <summary>
/// An address belonging to a <see cref="Registration"/>. It is part of the
/// Registration aggregate and cannot exist on its own. Governorate/City are
/// referenced by lookup id; the rule that a City must belong to the selected
/// Governorate is enforced in the Application layer where the lookups are loaded.
/// </summary>
public sealed partial class Address : Entity<Guid>
{
    public const int StreetMaxLength = 200;
    public const int BuildingNumberMaxLength = 20;
    public const int FlatNumberMaxLength = 20;

    // Real building/flat numbers may look like "12A" or "10/2": letters, digits,
    // slash, dash and spaces are allowed.
    [GeneratedRegex(@"^[A-Za-z0-9/\- ]+$", RegexOptions.CultureInvariant)]
    private static partial Regex BuildingOrFlatPattern();

    private Address(
        Guid id,
        int governorateId,
        int cityId,
        string street,
        string buildingNumber,
        string flatNumber,
        bool isPrimary) : base(id)
    {
        GovernorateId = governorateId;
        CityId = cityId;
        Street = street;
        BuildingNumber = buildingNumber;
        FlatNumber = flatNumber;
        IsPrimary = isPrimary;
    }

    private Address()
    {
    }

    public int GovernorateId { get; private set; }

    public int CityId { get; private set; }

    public string Street { get; private set; } = null!;

    public string BuildingNumber { get; private set; } = null!;

    public string FlatNumber { get; private set; } = null!;

    public bool IsPrimary { get; private set; }

    public static Address Create(
        int governorateId,
        int cityId,
        string? street,
        string? buildingNumber,
        string? flatNumber,
        bool isPrimary = false)
    {
        if (governorateId <= 0)
        {
            throw new DomainException("Governorate is required.");
        }

        if (cityId <= 0)
        {
            throw new DomainException("City is required.");
        }

        var normalizedStreet = (street ?? string.Empty).Trim();
        if (normalizedStreet.Length == 0)
        {
            throw new DomainException("Street is required.");
        }

        if (normalizedStreet.Length > StreetMaxLength)
        {
            throw new DomainException($"Street must not exceed {StreetMaxLength} characters.");
        }

        var normalizedBuilding = ValidateBuildingOrFlat(buildingNumber, "Building number", BuildingNumberMaxLength);
        var normalizedFlat = ValidateBuildingOrFlat(flatNumber, "Flat number", FlatNumberMaxLength);

        return new Address(
            Guid.NewGuid(),
            governorateId,
            cityId,
            normalizedStreet,
            normalizedBuilding,
            normalizedFlat,
            isPrimary);
    }

    internal void MarkAsPrimary() => IsPrimary = true;

    internal void UnmarkAsPrimary() => IsPrimary = false;

    private static string ValidateBuildingOrFlat(string? value, string field, int maxLength)
    {
        var normalized = (value ?? string.Empty).Trim();

        if (normalized.Length == 0)
        {
            throw new DomainException($"{field} is required.");
        }

        if (normalized.Length > maxLength)
        {
            throw new DomainException($"{field} must not exceed {maxLength} characters.");
        }

        if (!BuildingOrFlatPattern().IsMatch(normalized))
        {
            throw new DomainException(
                $"{field} may only contain letters, numbers, spaces, slashes and dashes.");
        }

        return normalized;
    }
}
