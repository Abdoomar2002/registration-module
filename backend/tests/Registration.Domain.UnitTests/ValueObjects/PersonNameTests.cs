using FluentAssertions;
using Registration.Domain.Common;
using Registration.Domain.Registrations.ValueObjects;

namespace Registration.Domain.UnitTests.ValueObjects;

public class PersonNameTests
{
    [Fact]
    public void Create_TrimsAndCollapsesRepeatedSpaces()
    {
        var name = PersonName.Create("  Abdel   rahman ", null, "  Omar ");

        name.First.Should().Be("Abdel rahman");
        name.Last.Should().Be("Omar");
        name.Middle.Should().BeNull();
        name.FullName.Should().Be("Abdel rahman Omar");
    }

    [Fact]
    public void Create_KeepsMiddleName_WhenProvided()
    {
        var name = PersonName.Create("Ahmed", "Mohamed", "Ali");

        name.Middle.Should().Be("Mohamed");
        name.FullName.Should().Be("Ahmed Mohamed Ali");
    }

    [Theory]
    [InlineData("Al-Sayed")]      // hyphen
    [InlineData("O'Brien")]       // apostrophe
    [InlineData("محمد")]          // Arabic letters
    [InlineData("عبد الرحمن")]    // Arabic with space
    public void Create_AllowsArabicEnglishAndSeparators(string value)
    {
        var act = () => PersonName.Create(value, null, "Omar");
        act.Should().NotThrow();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_Throws_WhenRequiredPartIsMissing(string? value)
    {
        var act = () => PersonName.Create(value, null, "Omar");
        act.Should().Throw<DomainException>().WithMessage("*First name*required*");
    }

    [Theory]
    [InlineData("Ahmed123")]      // digits
    [InlineData("Ahmed@")]        // special char
    [InlineData("Ahmed_Ali")]     // underscore
    public void Create_Throws_WhenPartContainsDisallowedCharacters(string value)
    {
        var act = () => PersonName.Create(value, null, "Omar");
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Create_Throws_WhenPartExceedsMaxLength()
    {
        var tooLong = new string('a', PersonName.MaxPartLength + 1);

        var act = () => PersonName.Create(tooLong, null, "Omar");

        act.Should().Throw<DomainException>().WithMessage("*50 characters*");
    }

    [Fact]
    public void Equality_IsStructural()
    {
        var a = PersonName.Create("Ahmed", "Mohamed", "Ali");
        var b = PersonName.Create("Ahmed", "Mohamed", "Ali");

        a.Should().Be(b);
        (a == b).Should().BeTrue();
    }
}
