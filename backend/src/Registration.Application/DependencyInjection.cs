using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Registration.Application.Common.Behaviors;

namespace Registration.Application;

/// <summary>Registers the Application layer services (CQRS, mapping, validation).</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            // Outermost first: unexpected-exception logging -> timing -> validation.
            configuration.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
