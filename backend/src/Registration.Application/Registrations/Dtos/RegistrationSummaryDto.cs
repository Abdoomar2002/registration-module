namespace Registration.Application.Registrations.Dtos;

/// <summary>A lightweight registration row for the paged list endpoint.</summary>
public sealed class RegistrationSummaryDto
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Mobile { get; init; } = string.Empty;

    public int AddressCount { get; init; }

    public DateTime CreatedAtUtc { get; init; }
}
