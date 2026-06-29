using MassTransit;
using Microsoft.Extensions.Logging;
using Registration.Application.Registrations.IntegrationEvents;
using Registration.Infrastructure.Notifications;

namespace Registration.Infrastructure.Consumers;

/// <summary>
/// Reacts to a committed registration by sending a welcome email and SMS. Runs
/// asynchronously (out of the create transaction); failures are retried by the
/// broker without affecting the original registration.
/// </summary>
public sealed class SendWelcomeNotificationConsumer : IConsumer<RegistrationCreatedIntegrationEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;
    private readonly ILogger<SendWelcomeNotificationConsumer> _logger;

    public SendWelcomeNotificationConsumer(
        IEmailSender emailSender,
        ISmsSender smsSender,
        ILogger<SendWelcomeNotificationConsumer> logger)
    {
        _emailSender = emailSender;
        _smsSender = smsSender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RegistrationCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation(
            "Sending welcome notifications for registration {RegistrationId}",
            message.RegistrationId);

        await _emailSender.SendAsync(
            message.Email,
            "Welcome to 3S",
            $"Hello {message.FullName}, your registration was created successfully.",
            context.CancellationToken);

        await _smsSender.SendAsync(
            message.Mobile,
            $"Welcome {message.FullName}! Your 3S registration is complete.",
            context.CancellationToken);
    }
}
