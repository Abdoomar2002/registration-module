using MassTransit;
using Registration.Application.Common.Interfaces;

namespace Registration.Infrastructure.Messaging;

/// <summary>
/// Publishes integration events via MassTransit. Because the bus outbox is
/// enabled, a Publish issued inside the active DbContext scope is stored in the
/// outbox table and delivered to RabbitMQ only after the transaction commits -
/// the create flow never depends on broker availability.
/// </summary>
public sealed class MassTransitIntegrationEventPublisher : IIntegrationEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitIntegrationEventPublisher(IPublishEndpoint publishEndpoint) =>
        _publishEndpoint = publishEndpoint;

    public Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : class =>
        _publishEndpoint.Publish(integrationEvent, cancellationToken);
}
