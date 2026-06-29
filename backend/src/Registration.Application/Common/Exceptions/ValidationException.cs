using FluentValidation.Results;

namespace Registration.Application.Common.Exceptions;

/// <summary>
/// Raised by the validation pipeline behavior when one or more FluentValidation
/// rules fail. The API maps it to a 400 ProblemDetails with the per-field errors.
/// </summary>
public sealed class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
            .ToDictionary(group => group.Key, group => group.Distinct().ToArray());
    }

    /// <summary>Validation messages keyed by the offending property name.</summary>
    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
