using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using Registration.Application.Lookups.Dtos;

namespace Registration.Application.Lookups.Queries.GetGovernorates;

public sealed class GetGovernoratesQueryHandler
    : IRequestHandler<GetGovernoratesQuery, IReadOnlyList<GovernorateDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetGovernoratesQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GovernorateDto>> Handle(
        GetGovernoratesQuery request,
        CancellationToken cancellationToken)
    {
        return await _db.Governorates
            .AsNoTracking()
            .Where(g => g.IsActive)
            .OrderBy(g => g.NameEn)
            .ProjectTo<GovernorateDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
