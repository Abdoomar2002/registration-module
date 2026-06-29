using Registration.Application.Registrations.Commands.CreateRegistration;

namespace Registration.IntegrationTests;

/// <summary>Builds unique, valid request payloads so tests don't collide on the unique indexes.</summary>
internal static class TestData
{
    private static int _counter;

    public static CreateRegistrationCommand ValidCommand(
        string? email = null,
        string? mobile = null,
        int governorateId = 1,
        int cityId = 1)
    {
        var n = Interlocked.Increment(ref _counter);

        return new CreateRegistrationCommand
        {
            FirstName = "Abdelrahman",
            MiddleName = "Mohamed",
            LastName = "Omar",
            BirthDate = new DateOnly(1998, 1, 1),
            Email = email ?? $"user{n}-{Guid.NewGuid():N}@example.com",
            Mobile = mobile ?? UniqueMobile(n),
            Addresses = new[]
            {
                new CreateAddressRequest
                {
                    GovernorateId = governorateId,
                    CityId = cityId,
                    Street = "Tahrir Street",
                    BuildingNumber = "12A",
                    FlatNumber = "3",
                    IsPrimary = true,
                },
            },
        };
    }

    public static string UniqueMobile(int seed) => $"+2010{(10_000_000 + seed) % 100_000_000:D8}";
}
