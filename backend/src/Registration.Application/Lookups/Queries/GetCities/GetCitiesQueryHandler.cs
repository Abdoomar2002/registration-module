using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using Registration.Application.Lookups.Dtos;

namespace Registration.Application.Lookups.Queries.GetCities;

public sealed class GetCitiesQueryHandler
    : IRequestHandler<GetCitiesQuery, IReadOnlyList<CityDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CityDto>> Handle(
        GetCitiesQuery request,
        CancellationToken cancellationToken)
    {
        return await _db.Cities
            .AsNoTracking()
            .Where(c => c.IsActive && c.GovernorateId == request.GovernorateId)
            .OrderBy(c => c.NameEn)
            .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
