using MediatR;

namespace Registration.Application.Registrations.Commands.CreateRegistration;

/// <summary>Command to create a new registration with one or more addresses.</summary>
public sealed record CreateRegistrationCommand : IRequest<CreateRegistrationResult>
{
    public string? FirstName { get; init; }

    public string? MiddleName { get; init; }

    public string? LastName { get; init; }

    public DateOnly BirthDate { get; init; }

    public string? Mobile { get; init; }

    public string? Email { get; init; }

    public IReadOnlyList<CreateAddressRequest> Addresses { get; init; } = Array.Empty<CreateAddressRequest>();
}

/// <summary>An address supplied as part of a create-registration request.</summary>
public sealed record CreateAddressRequest
{
    public int GovernorateId { get; init; }

    public int CityId { get; init; }

    public string? Street { get; init; }

    public string? BuildingNumber { get; init; }

    public string? FlatNumber { get; init; }

    public bool IsPrimary { get; init; }
}

/// <summary>The identifier of the newly created registration.</summary>
public sealed record CreateRegistrationResult(Guid Id);
