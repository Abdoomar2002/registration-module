using MediatR;
using Registration.Application.Registrations.Dtos;

namespace Registration.Application.Registrations.Queries.GetRegistrationById;

/// <summary>Returns full details of a registration, or null when it does not exist.</summary>
public sealed record GetRegistrationByIdQuery(Guid Id) : IRequest<RegistrationDetailsDto?>;
