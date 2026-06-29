using Registration.Application.Registrations.Commands.CreateRegistration;
using Registration.Application.Registrations.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Registration.Api.Swagger;

/// <summary>Example request body shown in Swagger for creating a registration.</summary>
public sealed class CreateRegistrationCommandExample : IExamplesProvider<CreateRegistrationCommand>
{
    public CreateRegistrationCommand GetExamples() => new()
    {
        FirstName = "Abdelrahman",
        MiddleName = "Mohamed",
        LastName = "Omar",
        BirthDate = new DateOnly(1998, 1, 1),
        Email = "abdo.omar@example.com",
        Mobile = "+201006158123",
        Addresses = new[]
        {
            new CreateAddressRequest
            {
                GovernorateId = 1,
                CityId = 1,
                Street = "Tahrir Street",
                BuildingNumber = "12A",
                FlatNumber = "3",
                IsPrimary = true,
            },
        },
    };
}

/// <summary>Example response body shown in Swagger for registration details.</summary>
public sealed class RegistrationDetailsDtoExample : IExamplesProvider<RegistrationDetailsDto>
{
    public RegistrationDetailsDto GetExamples() => new()
    {
        Id = Guid.Parse("3f2504e0-4f89-41d3-9a0c-0305e82c3301"),
        FirstName = "Abdelrahman",
        MiddleName = "Mohamed",
        LastName = "Omar",
        FullName = "Abdelrahman Mohamed Omar",
        BirthDate = new DateOnly(1998, 1, 1),
        Email = "abdo.omar@example.com",
        Mobile = "+201006158123",
        CreatedAtUtc = DateTime.UtcNow,
        Addresses = new[]
        {
            new AddressDetailsDto
            {
                Id = Guid.Parse("a1b2c3d4-1111-2222-3333-444455556666"),
                GovernorateId = 1,
                GovernorateName = "Cairo",
                CityId = 1,
                CityName = "Nasr City",
                Street = "Tahrir Street",
                BuildingNumber = "12A",
                FlatNumber = "3",
                IsPrimary = true,
            },
        },
    };
}
