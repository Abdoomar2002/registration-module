using FluentAssertions;
using Registration.Domain.Common;
using Registration.Domain.Registrations.ValueObjects;

namespace Registration.Domain.UnitTests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Create_KeepsOriginalAndNormalizesToLowerInvariant()
    {
        var email = Email.Create("  Abdo.Omar@Example.COM ");

        email.Value.Should().Be("Abdo.Omar@Example.COM");
        email.Normalized.Should().Be("abdo.omar@example.com");
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("missing@domain")]
    [InlineData("@no-local.com")]
    [InlineData("spaces in@email.com")]
    [InlineData("")]
    public void Create_Throws_WhenFormatIsInvalid(string value)
    {
        var act = () => Email.Create(value);
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Create_Throws_WhenExceedingMaxLength()
    {
        var local = new string('a', 250);
        var act = () => Email.Create($"{local}@x.com");

        act.Should().Throw<DomainException>().WithMessage("*254*");
    }

    [Fact]
    public void Equality_IsCaseInsensitive()
    {
        Email.Create("user@example.com").Should().Be(Email.Create("USER@EXAMPLE.COM"));
    }
}
