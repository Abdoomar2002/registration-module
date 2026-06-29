using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using Registration.Application.Common.Models;
using Registration.Application.Registrations.Dtos;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Application.Registrations.Queries.ListRegistrations;

/// <summary>
/// Returns a page of registration summaries. A deliberate SQL projection (rather
/// than AutoMapper) is used here so filtering, counting and paging happen in the
/// database and only the needed columns are read.
/// </summary>
public sealed class ListRegistrationsQueryHandler
    : IRequestHandler<ListRegistrationsQuery, PagedResult<RegistrationSummaryDto>>
{
    private readonly IApplicationDbContext _db;

    public ListRegistrationsQueryHandler(IApplicationDbContext db) => _db = db;

    public async Task<PagedResult<RegistrationSummaryDto>> Handle(
        ListRegistrationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _db.Registrations.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim();
            var normalizedEmail = term.ToLowerInvariant();

            query = query.Where(r =>
                r.Email.Normalized.Contains(normalizedEmail)
                || r.Mobile.Value.Contains(term)
                || r.Name.First.Contains(term)
                || r.Name.Last.Contains(term)
                || (r.Name.Middle != null && r.Name.Middle.Contains(term)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(r => r.CreatedAtUtc)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(r => new RegistrationSummaryDto
            {
                Id = r.Id,
                FullName = r.Name.Middle == null
                    ? r.Name.First + " " + r.Name.Last
                    : r.Name.First + " " + r.Name.Middle + " " + r.Name.Last,
                Email = r.Email.Value,
                Mobile = r.Mobile.Value,
                AddressCount = r.Addresses.Count,
                CreatedAtUtc = r.CreatedAtUtc,
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<RegistrationSummaryDto>(items, request.Page, request.PageSize, totalCount);
    }
}
