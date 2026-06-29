namespace Registration.Infrastructure.Messaging;

/// <summary>Strongly-typed RabbitMQ connection settings, bound from the "RabbitMq" section.</summary>
public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMq";

    public string Host { get; init; } = "localhost";

    public ushort Port { get; init; } = 5672;

    public string VirtualHost { get; init; } = "/";

    public string Username { get; init; } = "guest";

    public string Password { get; init; } = "guest";

    /// <summary>
    /// When true, the in-memory transport is used instead of RabbitMQ. Intended for
    /// integration tests so they need no external broker; the outbox still applies.
    /// </summary>
    public bool UseInMemory { get; init; }
}
