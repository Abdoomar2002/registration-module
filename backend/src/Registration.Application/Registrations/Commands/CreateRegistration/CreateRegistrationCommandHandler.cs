using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Exceptions;
using Registration.Application.Common.Interfaces;
using Registration.Application.Registrations.IntegrationEvents;
using Registration.Domain.Registrations;
using Registration.Domain.Registrations.ValueObjects;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Application.Registrations.Commands.CreateRegistration;

/// <summary>
/// Builds the Registration aggregate, enforces email/mobile uniqueness (409),
/// persists it, and publishes the integration event through the outbox so the
/// create transaction stays consistent and independent of broker availability.
/// </summary>
public sealed class CreateRegistrationCommandHandler
    : IRequestHandler<CreateRegistrationCommand, CreateRegistrationResult>
{
    private readonly IApplicationDbContext _db;
    private readonly IDateTimeProvider _dateTime;
    private readonly IIntegrationEventPublisher _publisher;

    public CreateRegistrationCommandHandler(
        IApplicationDbContext db,
        IDateTimeProvider dateTime,
        IIntegrationEventPublisher publisher)
    {
        _db = db;
        _dateTime = dateTime;
        _publisher = publisher;
    }

    public async Task<CreateRegistrationResult> Handle(
        CreateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        // Build value objects (today is injected for deterministic age rules).
        var name = PersonName.Create(request.FirstName, request.MiddleName, request.LastName);
        var birthDate = BirthDate.Create(request.BirthDate, _dateTime.Today);
        var email = Email.Create(request.Email);
        var mobile = MobileNumber.Create(request.Mobile);

        await EnsureUniqueAsync(email, mobile, cancellationToken);

        var addresses = request.Addresses
            .Select(a => Address.Create(a.GovernorateId, a.CityId, a.Street, a.BuildingNumber, a.FlatNumber, a.IsPrimary))
            .ToList();

        var registration = DomainRegistration.Create(name, birthDate, email, mobile, addresses);

        _db.Registrations.Add(registration);

        // Captured by the transactional outbox; delivered only after commit.
        await _publisher.PublishAsync(
            new RegistrationCreatedIntegrationEvent
            {
                RegistrationId = registration.Id,
                FullName = name.FullName,
                Email = email.Value,
                Mobile = mobile.Value,
                OccurredOnUtc = _dateTime.UtcNow,
            },
            cancellationToken);

        try
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            // A concurrent insert won the race; the unique index rejected ours.
            // Re-check to report the conflicting field (provider-agnostic).
            await EnsureUniqueAsync(email, mobile, cancellationToken);
            throw;
        }

        return new CreateRegistrationResult(registration.Id);
    }

    private async Task EnsureUniqueAsync(Email email, MobileNumber mobile, CancellationToken cancellationToken)
    {
        if (await _db.Registrations.AnyAsync(r => r.Email.Normalized == email.Normalized, cancellationToken))
        {
            throw DuplicateRegistrationException.ForEmail();
        }

        if (await _db.Registrations.AnyAsync(r => r.Mobile.Value == mobile.Value, cancellationToken))
        {
            throw DuplicateRegistrationException.ForMobile();
        }
    }
}
