using MediatR;
using Registration.Application.Lookups.Dtos;

namespace Registration.Application.Lookups.Queries.GetCities;

/// <summary>Returns the active cities that belong to a governorate (dependent dropdown).</summary>
public sealed record GetCitiesQuery(int GovernorateId) : IRequest<IReadOnlyList<CityDto>>;
