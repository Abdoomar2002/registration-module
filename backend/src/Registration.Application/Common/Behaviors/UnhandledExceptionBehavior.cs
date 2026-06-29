using MediatR;
using Microsoft.Extensions.Logging;
using Registration.Application.Common.Exceptions;

namespace Registration.Application.Common.Behaviors;

/// <summary>
/// Logs unexpected exceptions escaping a handler. Expected, gracefully-handled
/// exceptions (validation, not-found, duplicate) are excluded so they don't show
/// up as errors - the API translates those into the appropriate status codes.
/// </summary>
public sealed class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger) =>
        _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception exception) when (exception is not ValidationException
                                          and not NotFoundException
                                          and not DuplicateRegistrationException)
        {
            _logger.LogError(
                exception,
                "Unhandled exception while processing {RequestName}",
                typeof(TRequest).Name);
            throw;
        }
    }
}
