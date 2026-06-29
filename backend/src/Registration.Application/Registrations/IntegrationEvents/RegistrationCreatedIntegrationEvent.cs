namespace Registration.Application.Registrations.IntegrationEvents;

/// <summary>
/// Published to the message broker (through the outbox) after a registration is
/// committed. Downstream consumers use it to send a welcome email/SMS, write an
/// audit record, or notify other services - none of which block the create flow.
/// </summary>
public sealed record RegistrationCreatedIntegrationEvent
{
    public Guid RegistrationId { get; init; }

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Mobile { get; init; } = string.Empty;

    public DateTime OccurredOnUtc { get; init; }
}
