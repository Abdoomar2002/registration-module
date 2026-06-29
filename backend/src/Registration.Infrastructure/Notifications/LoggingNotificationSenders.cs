using Microsoft.Extensions.Logging;

namespace Registration.Infrastructure.Notifications;

/// <summary>
/// Stub email sender that logs instead of contacting a real provider. In a
/// production system this would be replaced by a SendGrid/SMTP implementation;
/// the interface keeps that swap isolated to the Infrastructure layer.
/// </summary>
public sealed class LoggingEmailSender : IEmailSender
{
    private readonly ILogger<LoggingEmailSender> _logger;

    public LoggingEmailSender(ILogger<LoggingEmailSender> logger) => _logger = logger;

    public Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[Email] To={Recipient} Subject={Subject}", to, subject);
        return Task.CompletedTask;
    }
}

/// <summary>Stub SMS sender that logs instead of contacting a real provider (e.g. Twilio).</summary>
public sealed class LoggingSmsSender : ISmsSender
{
    private readonly ILogger<LoggingSmsSender> _logger;

    public LoggingSmsSender(ILogger<LoggingSmsSender> logger) => _logger = logger;

    public Task SendAsync(string to, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[SMS] To={Recipient}", to);
        return Task.CompletedTask;
    }
}
