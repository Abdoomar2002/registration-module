using MediatR;
using Registration.Domain.Common;

namespace Registration.Application.Common.Messaging;

/// <summary>
/// Adapts a framework-free <see cref="IDomainEvent"/> to a MediatR
/// <see cref="INotification"/> so domain events can be dispatched in-process
/// without the Domain layer ever referencing MediatR.
/// </summary>
public sealed class DomainEventNotification<TDomainEvent> : INotification
    where TDomainEvent : IDomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent) => DomainEvent = domainEvent;

    public TDomainEvent DomainEvent { get; }
}
