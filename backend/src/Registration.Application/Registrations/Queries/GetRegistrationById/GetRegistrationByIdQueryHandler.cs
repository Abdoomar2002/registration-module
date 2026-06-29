using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using Registration.Application.Registrations.Dtos;

namespace Registration.Application.Registrations.Queries.GetRegistrationById;

public sealed class GetRegistrationByIdQueryHandler
    : IRequestHandler<GetRegistrationByIdQuery, RegistrationDetailsDto?>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetRegistrationByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<RegistrationDetailsDto?> Handle(
        GetRegistrationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var registration = await _db.Registrations
            .AsNoTracking()
            .Include(r => r.Addresses)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (registration is null)
        {
            return null;
        }

        var dto = _mapper.Map<RegistrationDetailsDto>(registration);
        await EnrichLookupNamesAsync(dto, cancellationToken);
        return dto;
    }

    private async Task EnrichLookupNamesAsync(RegistrationDetailsDto dto, CancellationToken cancellationToken)
    {
        var governorateIds = dto.Addresses.Select(a => a.GovernorateId).Distinct().ToList();
        var cityIds = dto.Addresses.Select(a => a.CityId).Distinct().ToList();

        var governorates = await _db.Governorates
            .AsNoTracking()
            .Where(g => governorateIds.Contains(g.Id))
            .ToDictionaryAsync(g => g.Id, g => g.NameEn, cancellationToken);

        var cities = await _db.Cities
            .AsNoTracking()
            .Where(c => cityIds.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id, c => c.NameEn, cancellationToken);

        foreach (var address in dto.Addresses)
        {
            address.GovernorateName = governorates.GetValueOrDefault(address.GovernorateId);
            address.CityName = cities.GetValueOrDefault(address.CityId);
        }
    }
}
