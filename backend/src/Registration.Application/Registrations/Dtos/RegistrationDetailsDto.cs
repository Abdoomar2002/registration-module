namespace Registration.Application.Registrations.Dtos;

/// <summary>Full registration details returned by GET /api/registrations/{id}.</summary>
public sealed class RegistrationDetailsDto
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string? MiddleName { get; init; }

    public string LastName { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public DateOnly BirthDate { get; init; }

    public string Email { get; init; } = string.Empty;

    public string Mobile { get; init; } = string.Empty;

    public IReadOnlyList<AddressDetailsDto> Addresses { get; set; } = Array.Empty<AddressDetailsDto>();

    public DateTime CreatedAtUtc { get; init; }

    public DateTime? UpdatedAtUtc { get; init; }
}
