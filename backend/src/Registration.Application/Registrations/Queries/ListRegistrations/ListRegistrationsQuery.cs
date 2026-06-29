using MediatR;
using Registration.Application.Common.Models;
using Registration.Application.Registrations.Dtos;

namespace Registration.Application.Registrations.Queries.ListRegistrations;

/// <summary>Paged, optionally searched list of registrations (bonus endpoint).</summary>
public sealed record ListRegistrationsQuery(int Page = 1, int PageSize = 20, string? Search = null)
    : IRequest<PagedResult<RegistrationSummaryDto>>;
