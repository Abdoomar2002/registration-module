using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Registration.Application.Common.Behaviors;

/// <summary>
/// Logs the name and duration of every request. It deliberately never logs the
/// request payload, since requests carry personal data; the correlation id is
/// already attached to the log scope by the API middleware.
/// </summary>
public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("Handling {RequestName}", requestName);

        try
        {
            var response = await next();
            stopwatch.Stop();
            _logger.LogInformation(
                "Handled {RequestName} in {ElapsedMilliseconds} ms",
                requestName,
                stopwatch.ElapsedMilliseconds);
            return response;
        }
        catch
        {
            stopwatch.Stop();
            _logger.LogWarning(
                "{RequestName} failed after {ElapsedMilliseconds} ms",
                requestName,
                stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
