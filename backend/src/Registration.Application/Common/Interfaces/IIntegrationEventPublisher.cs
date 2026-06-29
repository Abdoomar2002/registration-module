namespace Registration.Application.Common.Interfaces;

/// <summary>
/// Publishes integration events to other services. The Infrastructure layer
/// implements this over MassTransit + the transactional outbox, so the Application
/// stays free of any specific message-broker dependency.
/// </summary>
/// <remarks>
/// When called inside an active DbContext transaction, the outbox captures the
/// message and delivers it only after the transaction commits - the create
/// transaction never depends on broker availability.
/// </remarks>
public interface IIntegrationEventPublisher
{
    Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : class;
}
