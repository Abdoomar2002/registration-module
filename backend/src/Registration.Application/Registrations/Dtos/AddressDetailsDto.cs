namespace Registration.Application.Registrations.Dtos;

/// <summary>An address as returned in registration details, including lookup names.</summary>
public sealed class AddressDetailsDto
{
    public Guid Id { get; init; }

    public int GovernorateId { get; init; }

    public string? GovernorateName { get; set; }

    public int CityId { get; init; }

    public string? CityName { get; set; }

    public string Street { get; init; } = string.Empty;

    public string BuildingNumber { get; init; } = string.Empty;

    public string FlatNumber { get; init; } = string.Empty;

    public bool IsPrimary { get; init; }
}
