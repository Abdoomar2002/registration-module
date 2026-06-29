namespace Registration.Domain.Common;

/// <summary>
/// Marker for an in-process domain event. Deliberately free of any framework
/// type (no MediatR) so the Domain layer keeps zero infrastructure dependencies.
/// The Application layer adapts these to MediatR notifications when dispatching.
/// </summary>
public interface IDomainEvent
{
    /// <summary>UTC instant at which the event was raised.</summary>
    DateTime OccurredOnUtc { get; }
}
