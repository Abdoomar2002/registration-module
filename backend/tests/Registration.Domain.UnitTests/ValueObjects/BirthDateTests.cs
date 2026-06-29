using FluentAssertions;
using Registration.Domain.Common;
using Registration.Domain.Registrations.ValueObjects;

namespace Registration.Domain.UnitTests.ValueObjects;

public class BirthDateTests
{
    private static readonly DateOnly Today = new(2026, 6, 29);

    [Fact]
    public void Create_Throws_WhenInTheFuture()
    {
        var act = () => BirthDate.Create(Today.AddDays(1), Today);
        act.Should().Throw<DomainException>().WithMessage("*future*");
    }

    [Fact]
    public void Create_Throws_WhenYoungerThanMinimumAge()
    {
        // One day short of the 20th birthday.
        var birth = Today.AddYears(-BirthDate.MinimumAgeYears).AddDays(1);

        var act = () => BirthDate.Create(birth, Today);

        act.Should().Throw<DomainException>().WithMessage("*at least 20 years*");
    }

    [Fact]
    public void Create_Succeeds_ExactlyOnTwentiethBirthday()
    {
        var birth = Today.AddYears(-BirthDate.MinimumAgeYears);

        var act = () => BirthDate.Create(birth, Today);

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(2000, 6, 29, 26)] // birthday already happened (on the day)
    [InlineData(2000, 6, 30, 25)] // birthday is tomorrow -> still 25
    [InlineData(2000, 12, 31, 25)]
    public void AgeOn_CalculatesAccurately(int year, int month, int day, int expectedAge)
    {
        var birthDate = BirthDate.Create(new DateOnly(year, month, day), Today);

        birthDate.AgeOn(Today).Should().Be(expectedAge);
    }
}
