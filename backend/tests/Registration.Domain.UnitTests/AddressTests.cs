using FluentAssertions;
using Registration.Domain.Common;
using Registration.Domain.Registrations;

namespace Registration.Domain.UnitTests;

public class AddressTests
{
    [Theory]
    [InlineData("12A")]
    [InlineData("10/2")]
    [InlineData("Block 5 - Apt 3")]
    public void Create_AllowsRealisticBuildingNumbers(string buildingNumber)
    {
        var act = () => Address.Create(1, 1, "Tahrir Street", buildingNumber, "3", isPrimary: true);
        act.Should().NotThrow();
    }

    [Fact]
    public void Create_TrimsStreet()
    {
        var address = Address.Create(1, 1, "  Tahrir Street  ", "12A", "3");
        address.Street.Should().Be("Tahrir Street");
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void Create_Throws_WhenLookupIdMissing(int governorateId, int cityId)
    {
        var act = () => Address.Create(governorateId, cityId, "Street", "1", "1");
        act.Should().Throw<DomainException>();
    }

    [Theory]
    [InlineData("12#")]   // disallowed symbol
    [InlineData("12*3")]
    public void Create_Throws_WhenBuildingNumberHasDisallowedCharacters(string buildingNumber)
    {
        var act = () => Address.Create(1, 1, "Street", buildingNumber, "1");
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Create_Throws_WhenStreetMissing()
    {
        var act = () => Address.Create(1, 1, "   ", "1", "1");
        act.Should().Throw<DomainException>().WithMessage("*Street is required*");
    }
}
