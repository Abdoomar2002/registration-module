using Registration.Domain.Common;

namespace Registration.Domain.Registrations.Events;

/// <summary>
/// Raised when a new <see cref="Registration"/> aggregate is created. Dispatched
/// in-process (via MediatR adapter) for side effects such as audit logging. The
/// cross-service integration event is published separately through the outbox.
/// </summary>
public sealed record RegistrationCreatedDomainEvent(
    Guid RegistrationId,
    string NormalizedEmail,
    string Mobile,
    DateTime OccurredOnUtc) : IDomainEvent;
