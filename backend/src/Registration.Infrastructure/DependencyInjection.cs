using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registration.Application.Common.Interfaces;
using Registration.Infrastructure.Messaging;
using Registration.Infrastructure.Notifications;
using Registration.Infrastructure.Time;
using Registration.Persistence;

namespace Registration.Infrastructure;

/// <summary>
/// Registers cross-cutting infrastructure: the clock, notification senders, and
/// the MassTransit/RabbitMQ bus with the EF Core transactional outbox.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IIntegrationEventPublisher, MassTransitIntegrationEventPublisher>();
        services.AddScoped<IEmailSender, LoggingEmailSender>();
        services.AddScoped<ISmsSender, LoggingSmsSender>();

        services.AddOptions<RabbitMqOptions>().Bind(configuration.GetSection(RabbitMqOptions.SectionName));
        var rabbit = configuration.GetSection(RabbitMqOptions.SectionName).Get<RabbitMqOptions>() ?? new RabbitMqOptions();

        services.AddMassTransit(bus =>
        {
            bus.SetKebabCaseEndpointNameFormatter();
            bus.AddConsumers(typeof(DependencyInjection).Assembly);

            // Transactional outbox: integration events are written with the
            // registration in one transaction and delivered to RabbitMQ afterwards.
            bus.AddEntityFrameworkOutbox<ApplicationDbContext>(outbox =>
            {
                outbox.UseSqlServer();
                outbox.UseBusOutbox();
                outbox.QueryDelay = TimeSpan.FromSeconds(5);
            });

            bus.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbit.Host, rabbit.Port, rabbit.VirtualHost, host =>
                {
                    host.Username(rabbit.Username);
                    host.Password(rabbit.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
