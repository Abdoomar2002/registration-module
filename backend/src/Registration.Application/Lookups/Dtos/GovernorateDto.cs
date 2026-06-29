namespace Registration.Application.Lookups.Dtos;

/// <summary>A governorate option for the lookup dropdowns.</summary>
public sealed class GovernorateDto
{
    public int Id { get; init; }

    public string NameEn { get; init; } = string.Empty;

    public string NameAr { get; init; } = string.Empty;
}
