namespace Registration.Domain.Lookups;

/// <summary>
/// A city lookup that belongs to a single <see cref="Governorate"/>. Names are
/// kept in English and Arabic. The City dropdown in the UI is filtered by the
/// selected governorate, and the same relationship is validated server-side.
/// </summary>
public sealed class City
{
    public City(int id, int governorateId, string nameEn, string nameAr, bool isActive = true)
    {
        Id = id;
        GovernorateId = governorateId;
        NameEn = nameEn;
        NameAr = nameAr;
        IsActive = isActive;
    }

    private City()
    {
    }

    public int Id { get; private set; }

    public int GovernorateId { get; private set; }

    public string NameEn { get; private set; } = null!;

    public string NameAr { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public Governorate? Governorate { get; private set; }
}
