namespace Registration.Infrastructure.Notifications;

/// <summary>Sends transactional emails (welcome message, etc.).</summary>
public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
}

/// <summary>Sends SMS messages.</summary>
public interface ISmsSender
{
    Task SendAsync(string to, string message, CancellationToken cancellationToken = default);
}
