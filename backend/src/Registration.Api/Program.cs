using System.Reflection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Registration.Api.Common;
using Registration.Api.Middleware;
using Registration.Api.Swagger;
using Registration.Application;
using Registration.Application.Common.Interfaces;
using Registration.Infrastructure;
using Registration.Persistence;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Structured logging (Serilog) with correlation id support via the log context.
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// Clean Architecture layers.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Consistent RFC 7807 error responses.
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Registration API",
        Version = "v1",
        Description = "Registration module API - create registrations with addresses and read lookups.",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    options.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateRegistrationCommandExample>();

// Health checks: SQL Server explicitly; MassTransit auto-registers a bus check (RabbitMQ).
var connectionString = builder.Configuration.GetConnectionString("Default")!;
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, name: "sqlserver", tags: new[] { "ready" });

var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .WithOrigins(corsOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders(CorrelationIdMiddleware.HeaderName)));

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Registration API v1"));
}

app.UseCors();
app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

await app.MigrateDatabaseAsync();

app.Run();

// Exposed so the integration test project can use WebApplicationFactory<Program>.
public partial class Program;
