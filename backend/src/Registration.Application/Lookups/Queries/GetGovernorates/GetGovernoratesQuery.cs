using MediatR;
using Registration.Application.Lookups.Dtos;

namespace Registration.Application.Lookups.Queries.GetGovernorates;

/// <summary>Returns the active governorates for the lookup dropdown.</summary>
public sealed record GetGovernoratesQuery : IRequest<IReadOnlyList<GovernorateDto>>;
