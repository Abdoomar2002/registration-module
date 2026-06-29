using MassTransit;
using Microsoft.Extensions.Logging;
using Registration.Application.Registrations.IntegrationEvents;

namespace Registration.Infrastructure.Consumers;

/// <summary>
/// Writes an audit trail entry for each registration. Here it logs a structured
/// audit record; a real system might persist it to an audit store or forward it
/// to another service. No sensitive personal data beyond identifiers is recorded.
/// </summary>
public sealed class RegistrationAuditConsumer : IConsumer<RegistrationCreatedIntegrationEvent>
{
    private readonly ILogger<RegistrationAuditConsumer> _logger;

    public RegistrationAuditConsumer(ILogger<RegistrationAuditConsumer> logger) => _logger = logger;

    public Task Consume(ConsumeContext<RegistrationCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation(
            "AUDIT: registration {RegistrationId} created at {OccurredOnUtc:o}",
            message.RegistrationId,
            message.OccurredOnUtc);

        return Task.CompletedTask;
    }
}
