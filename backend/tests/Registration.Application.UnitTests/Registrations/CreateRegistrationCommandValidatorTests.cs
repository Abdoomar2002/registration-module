using FluentValidation.TestHelper;
using Registration.Application.Registrations.Commands.CreateRegistration;
using Registration.Application.UnitTests.TestSupport;

namespace Registration.Application.UnitTests.Registrations;

public class CreateRegistrationCommandValidatorTests
{
    private readonly TestApplicationDbContext _db = TestHarness.CreateContext();
    private CreateRegistrationCommandValidator CreateValidator() =>
        new(_db, new FixedDateTimeProvider(TestHarness.Today));

    [Fact]
    public async Task Valid_Command_Passes()
    {
        var result = await CreateValidator().TestValidateAsync(ValidCommand());
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Missing_FirstName_Fails()
    {
        var result = await CreateValidator().TestValidateAsync(ValidCommand() with { FirstName = "" });
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public async Task Name_With_Digits_Fails()
    {
        var result = await CreateValidator().TestValidateAsync(ValidCommand() with { LastName = "Omar123" });
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public async Task Underage_Fails()
    {
        var underage = TestHarness.Today.AddYears(-19);
        var result = await CreateValidator().TestValidateAsync(ValidCommand() with { BirthDate = underage });
        result.ShouldHaveValidationErrorFor(x => x.BirthDate);
    }

    [Fact]
    public async Task Invalid_Email_Fails()
    {
        var result = await CreateValidator().TestValidateAsync(ValidCommand() with { Email = "not-an-email" });
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Invalid_Mobile_Fails()
    {
        var result = await CreateValidator().TestValidateAsync(ValidCommand() with { Mobile = "01006158123" });
        result.ShouldHaveValidationErrorFor(x => x.Mobile);
    }

    [Fact]
    public async Task City_Not_Belonging_To_Governorate_Fails()
    {
        // City 3 (Dokki) belongs to governorate 2 (Giza), not 1 (Cairo).
        var command = ValidCommand() with
        {
            Addresses = new[] { Address(governorateId: 1, cityId: 3) },
        };

        var result = await CreateValidator().TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor("Addresses[0].CityId");
    }

    [Fact]
    public async Task Nonexistent_Governorate_Fails()
    {
        var command = ValidCommand() with { Addresses = new[] { Address(governorateId: 99, cityId: 1) } };
        var result = await CreateValidator().TestValidateAsync(command);
        result.ShouldHaveValidationErrorFor("Addresses[0].GovernorateId");
    }

    [Fact]
    public async Task Too_Many_Addresses_Fails()
    {
        var addresses = Enumerable.Range(0, 6).Select(i => Address(1, 1, isPrimary: i == 0)).ToArray();
        var result = await CreateValidator().TestValidateAsync(ValidCommand() with { Addresses = addresses });
        result.ShouldHaveValidationErrorFor(x => x.Addresses);
    }

    [Fact]
    public async Task Multiple_Primary_Addresses_Fails()
    {
        var command = ValidCommand() with
        {
            Addresses = new[] { Address(1, 1, isPrimary: true), Address(1, 2, isPrimary: true) },
        };

        var result = await CreateValidator().TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Addresses);
    }

    private static CreateRegistrationCommand ValidCommand() => new()
    {
        FirstName = "Abdelrahman",
        LastName = "Omar",
        BirthDate = new DateOnly(1998, 1, 1),
        Email = "abdo@example.com",
        Mobile = "+201006158123",
        Addresses = new[] { Address(1, 1, isPrimary: true) },
    };

    private static CreateAddressRequest Address(int governorateId, int cityId, bool isPrimary = false) => new()
    {
        GovernorateId = governorateId,
        CityId = cityId,
        Street = "Tahrir Street",
        BuildingNumber = "12A",
        FlatNumber = "3",
        IsPrimary = isPrimary,
    };
}
