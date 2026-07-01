using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Registration.Application.Common.Interfaces;
using Registration.Presentation.Common;
using Registration.Presentation.Middleware;
using Registration.Presentation.Swagger;
using Swashbuckle.AspNetCore.Filters;

namespace Registration.Presentation;

/// <summary>
/// Registers the Presentation layer services: controllers, Swagger/OpenAPI,
/// ProblemDetails, exception handling, current-user provider, and middleware.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        services.AddEndpointsApiExplorer();

        // Consistent RFC 7807 error responses.
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "Registration API",
                Version = "v1",
                Description = "Registration module API - create registrations with addresses and read lookups.",
            });

            var xmlFile = $"{typeof(DependencyInjection).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            options.ExampleFilters();
        });
        services.AddSwaggerExamplesFromAssemblyOf<CreateRegistrationCommandExample>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;
    }

    /// <summary>
    /// Adds the Presentation middleware to the pipeline. Call after
    /// <c>UseSerilogRequestLogging()</c> and before <c>MapControllers()</c>.
    /// </summary>
    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseExceptionHandler();

        return app;
    }
}
