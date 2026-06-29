namespace Registration.Domain.Common;

/// <summary>
/// Thrown when a domain invariant is violated. This is the last line of defense:
/// user-facing input is validated earlier (FluentValidation), so reaching this
/// exception generally indicates a bug or a bypassed validation path.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
