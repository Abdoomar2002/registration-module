namespace Registration.Application.Lookups.Dtos;

/// <summary>A city option, scoped to its governorate, for the dependent dropdown.</summary>
public sealed class CityDto
{
    public int Id { get; init; }

    public int GovernorateId { get; init; }

    public string NameEn { get; init; } = string.Empty;

    public string NameAr { get; init; } = string.Empty;
}
