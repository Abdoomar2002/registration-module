using AutoMapper;
using FluentAssertions;
using Registration.Application.Common.Mappings;
using Registration.Application.Registrations.Dtos;
using Registration.Domain.Registrations;
using Registration.Domain.Registrations.ValueObjects;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Application.UnitTests.Mappings;

public class MappingProfileTests
{
    private readonly MapperConfiguration _configuration = new(c => c.AddProfile<RegistrationMappingProfile>());

    [Fact]
    public void MappingConfiguration_IsValid()
    {
        // Acceptance criteria: mapping configuration must be tested for validity.
        _configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void Maps_Registration_To_DetailsDto_FlatteningValueObjects()
    {
        var mapper = _configuration.CreateMapper();
        var registration = BuildRegistration();

        var dto = mapper.Map<RegistrationDetailsDto>(registration);

        dto.FirstName.Should().Be("Ahmed");
        dto.MiddleName.Should().Be("Mohamed");
        dto.LastName.Should().Be("Ali");
        dto.FullName.Should().Be("Ahmed Mohamed Ali");
        dto.Email.Should().Be("ahmed@example.com");
        dto.Mobile.Should().Be("+201006158123");
        dto.BirthDate.Should().Be(new DateOnly(1998, 1, 1));
        dto.Addresses.Should().ContainSingle();
        dto.Addresses[0].IsPrimary.Should().BeTrue();
    }

    [Fact]
    public void Maps_Registration_To_SummaryDto()
    {
        var mapper = _configuration.CreateMapper();

        var dto = mapper.Map<RegistrationSummaryDto>(BuildRegistration());

        dto.FullName.Should().Be("Ahmed Mohamed Ali");
        dto.Email.Should().Be("ahmed@example.com");
        dto.AddressCount.Should().Be(1);
    }

    private static DomainRegistration BuildRegistration() =>
        DomainRegistration.Create(
            PersonName.Create("Ahmed", "Mohamed", "Ali"),
            BirthDate.Create(new DateOnly(1998, 1, 1), new DateOnly(2026, 6, 29)),
            Email.Create("ahmed@example.com"),
            MobileNumber.Create("+201006158123"),
            new[] { Address.Create(1, 1, "Tahrir Street", "12A", "3", isPrimary: true) });
}
