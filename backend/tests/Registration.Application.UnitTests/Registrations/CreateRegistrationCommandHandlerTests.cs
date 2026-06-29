using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Exceptions;
using Registration.Application.Registrations.Commands.CreateRegistration;
using Registration.Application.Registrations.IntegrationEvents;
using Registration.Application.UnitTests.TestSupport;

namespace Registration.Application.UnitTests.Registrations;

public class CreateRegistrationCommandHandlerTests
{
    private readonly TestApplicationDbContext _db = TestHarness.CreateContext();
    private readonly RecordingIntegrationEventPublisher _publisher = new();

    private CreateRegistrationCommandHandler CreateHandler() =>
        new(_db, new FixedDateTimeProvider(TestHarness.Today), _publisher);

    [Fact]
    public async Task Handle_PersistsRegistration_AndPublishesIntegrationEvent()
    {
        var result = await CreateHandler().Handle(ValidCommand(), CancellationToken.None);

        result.Id.Should().NotBeEmpty();

        var saved = await _db.Registrations.Include(r => r.Addresses).SingleAsync();
        saved.Id.Should().Be(result.Id);
        saved.Email.Normalized.Should().Be("abdo@example.com");
        saved.Addresses.Should().ContainSingle(a => a.IsPrimary);

        _publisher.Published.Should().ContainSingle()
            .Which.Should().BeOfType<RegistrationCreatedIntegrationEvent>()
            .Which.RegistrationId.Should().Be(result.Id);
    }

    [Fact]
    public async Task Handle_Throws_OnDuplicateEmail()
    {
        await CreateHandler().Handle(ValidCommand(), CancellationToken.None);

        var duplicate = ValidCommand() with { Mobile = "+201111111111", Email = "ABDO@example.com" };

        var act = () => CreateHandler().Handle(duplicate, CancellationToken.None);

        (await act.Should().ThrowAsync<DuplicateRegistrationException>()).Which.Field.Should().Be("email");
    }

    [Fact]
    public async Task Handle_Throws_OnDuplicateMobile()
    {
        await CreateHandler().Handle(ValidCommand(), CancellationToken.None);

        var duplicate = ValidCommand() with { Email = "other@example.com", Mobile = "+201006158123" };

        var act = () => CreateHandler().Handle(duplicate, CancellationToken.None);

        (await act.Should().ThrowAsync<DuplicateRegistrationException>()).Which.Field.Should().Be("mobile");
    }

    private static CreateRegistrationCommand ValidCommand() => new()
    {
        FirstName = "Abdelrahman",
        LastName = "Omar",
        BirthDate = new DateOnly(1998, 1, 1),
        Email = "abdo@example.com",
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
