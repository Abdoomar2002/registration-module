using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Registration.Application.Common.Exceptions;
using Registration.Domain.Common;
using ValidationException = Registration.Application.Common.Exceptions.ValidationException;

namespace Registration.Presentation.Common;

/// <summary>
/// Translates exceptions into RFC 7807 ProblemDetails with a consistent shape:
/// 400 for validation/domain-rule errors, 404 for not-found, 409 for duplicate
/// email/mobile, and 500 for anything unexpected (whose details are not leaked).
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = BuildProblemDetails(httpContext, exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        if (problemDetails.Status >= 500)
        {
            _logger.LogError(exception, "Unhandled exception ({CorrelationId})", httpContext.TraceIdentifier);
        }
        else
        {
            _logger.LogWarning(
                "Request failed with {StatusCode}: {Message} ({CorrelationId})",
                problemDetails.Status,
                exception.Message,
                httpContext.TraceIdentifier);
        }

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails,
        });
    }

    private static ProblemDetails BuildProblemDetails(HttpContext httpContext, Exception exception)
    {
        ProblemDetails problemDetails = exception switch
        {
            ValidationException validation => new ValidationProblemDetails(
                validation.Errors.ToDictionary(e => e.Key, e => e.Value))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred.",
            },

            DuplicateRegistrationException duplicate => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = duplicate.Message,
                Extensions = { ["field"] = duplicate.Field },
            },

            NotFoundException notFound => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = notFound.Message,
            },

            DomainException domain => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Domain rule violation",
                Detail = domain.Message,
            },

            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = "An unexpected error occurred. Please contact support with the correlation id.",
            },
        };

        problemDetails.Extensions["correlationId"] = httpContext.TraceIdentifier;
        return problemDetails;
    }
}
