namespace Registration.Application.Common.Exceptions;

/// <summary>
/// Raised when a registration would duplicate an existing Email or Mobile Number.
/// Mapped to 409 Conflict by the API (distinct from 400 validation errors).
/// </summary>
public sealed class DuplicateRegistrationException : Exception
{
    public DuplicateRegistrationException(string field, string message) : base(message)
    {
        Field = field;
    }

    /// <summary>The conflicting field: "email" or "mobile".</summary>
    public string Field { get; }

    public static DuplicateRegistrationException ForEmail() =>
        new("email", "A registration with this email already exists.");

    public static DuplicateRegistrationException ForMobile() =>
        new("mobile", "A registration with this mobile number already exists.");
}
