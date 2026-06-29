using FluentAssertions;
using Registration.Domain.Common;
using Registration.Domain.Registrations;
using Registration.Domain.Registrations.Events;
using Registration.Domain.Registrations.ValueObjects;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Domain.UnitTests;

public class RegistrationTests
{
    private static readonly DateOnly Today = new(2026, 6, 29);

    [Fact]
    public void Create_WithSingleAddress_MarksItPrimaryAutomatically()
    {
        var registration = CreateRegistration(Address("1", isPrimary: false));

        registration.Addresses.Should().ContainSingle();
        registration.PrimaryAddress.Should().Be(registration.Addresses.Single());
        registration.Addresses.Single().IsPrimary.Should().BeTrue();
    }

    [Fact]
    public void Create_RaisesRegistrationCreatedDomainEvent()
    {
        var registration = CreateRegistration(Address("1"));

        registration.DomainEvents.Should().ContainSingle()
            .Which.Should().BeOfType<RegistrationCreatedDomainEvent>()
            .Which.RegistrationId.Should().Be(registration.Id);
    }

    [Fact]
    public void Create_Throws_WhenNoAddresses()
    {
        var act = () => CreateRegistration();
        act.Should().Throw<DomainException>().WithMessage("*at least one address*");
    }

    [Fact]
    public void Create_Throws_WhenMoreThanMaxAddresses()
    {
        var addresses = Enumerable.Range(1, DomainRegistration.MaxAddresses + 1)
            .Select(i => Address(i.ToString(), isPrimary: i == 1))
            .ToArray();

        var act = () => CreateRegistration(addresses);

        act.Should().Throw<DomainException>().WithMessage("*maximum of 5*");
    }

    [Fact]
    public void Create_Throws_WhenMultipleAddressesAndNonePrimary()
    {
        var act = () => CreateRegistration(Address("1", false), Address("2", false));
        act.Should().Throw<DomainException>().WithMessage("*must be marked as primary*");
    }

    [Fact]
    public void Create_Throws_WhenMoreThanOnePrimary()
    {
        var act = () => CreateRegistration(Address("1", true), Address("2", true));
        act.Should().Throw<DomainException>().WithMessage("*Only one address*primary*");
    }

    private static DomainRegistration CreateRegistration(params Address[] addresses) =>
        DomainRegistration.Create(
            PersonName.Create("Abdelrahman", null, "Omar"),
            BirthDate.Create(new DateOnly(1998, 1, 1), Today),
            Email.Create("abdo@example.com"),
            MobileNumber.Create("+201006158123"),
            addresses);

    private static Address Address(string building, bool isPrimary = false) =>
        Registration.Domain.Registrations.Address.Create(1, 1, "Tahrir Street", building, "3", isPrimary);
}
