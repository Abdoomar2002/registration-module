using Registration.Domain.Lookups;

namespace Registration.Persistence.Seeding;

/// <summary>
/// Fixed seed data for the Governorate and City lookups, applied through EF Core
/// migrations (HasData). Identifiers are stable so they can be referenced safely.
/// </summary>
public static class LookupSeedData
{
    public static IReadOnlyList<Governorate> Governorates { get; } = new[]
    {
        new Governorate(1, "Cairo", "القاهرة"),
        new Governorate(2, "Giza", "الجيزة"),
        new Governorate(3, "Alexandria", "الإسكندرية"),
    };

    public static IReadOnlyList<City> Cities { get; } = new[]
    {
        // Cairo
        new City(1, 1, "Nasr City", "مدينة نصر"),
        new City(2, 1, "Maadi", "المعادي"),
        new City(3, 1, "Heliopolis", "مصر الجديدة"),
        // Giza
        new City(4, 2, "Dokki", "الدقي"),
        new City(5, 2, "6th of October", "السادس من أكتوبر"),
        new City(6, 2, "Haram", "الهرم"),
        // Alexandria
        new City(7, 3, "Smouha", "سموحة"),
        new City(8, 3, "Miami", "ميامي"),
    };
}
