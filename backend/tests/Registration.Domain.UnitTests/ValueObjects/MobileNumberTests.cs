using FluentAssertions;
using Registration.Domain.Common;
using Registration.Domain.Registrations.ValueObjects;

namespace Registration.Domain.UnitTests.ValueObjects;

public class MobileNumberTests
{
    [Theory]
    [InlineData("+201006158123", "+201006158123")]
    [InlineData("+20 100 615 8123", "+201006158123")] // spaces stripped
    [InlineData("+20-100-615-8123", "+201006158123")] // hyphens stripped
    public void Create_NormalizesToE164(string input, string expected)
    {
        Mobile.Create(input).Value.Should().Be(expected);
    }

    [Theory]
    [InlineData("01006158123")]    // missing country code / no '+'
    [InlineData("+0201006158")]    // leading zero after '+'
    [InlineData("+2010")]          // too short
    [InlineData("+abc123456789")]  // letters
    [InlineData("")]
    public void Create_Throws_WhenNotValidE164(string value)
    {
        var act = () => Mobile.Create(value);
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Equality_ComparesNormalizedValue()
    {
        Mobile.Create("+20 100 615 8123").Should().Be(Mobile.Create("+201006158123"));
    }

    // Local alias to keep test lines short.
    private static class Mobile
    {
        public static MobileNumber Create(string value) => MobileNumber.Create(value);
    }
}
