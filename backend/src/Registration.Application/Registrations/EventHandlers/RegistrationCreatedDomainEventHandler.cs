using MediatR;
using Microsoft.Extensions.Logging;
using Registration.Application.Common.Messaging;
using Registration.Domain.Registrations.Events;

namespace Registration.Application.Registrations.EventHandlers;

/// <summary>
/// In-process handler for the RegistrationCreated domain event. Kept intentionally
/// side-effect-light (structured logging only); cross-service work happens via the
/// integration event published through the outbox. No personal data is logged.
/// </summary>
public sealed class RegistrationCreatedDomainEventHandler
    : INotificationHandler<DomainEventNotification<RegistrationCreatedDomainEvent>>
{
    private readonly ILogger<RegistrationCreatedDomainEventHandler> _logger;

    public RegistrationCreatedDomainEventHandler(ILogger<RegistrationCreatedDomainEventHandler> logger) =>
        _logger = logger;

    public Task Handle(
        DomainEventNotification<RegistrationCreatedDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation(
            "Registration {RegistrationId} created at {OccurredOnUtc:o}",
            domainEvent.RegistrationId,
            domainEvent.OccurredOnUtc);

        return Task.CompletedTask;
    }
}
