using AutoMapper;
using Registration.Application.Lookups.Dtos;
using Registration.Application.Registrations.Dtos;
using Registration.Domain.Lookups;
using DomainAddress = Registration.Domain.Registrations.Address;
using DomainRegistration = Registration.Domain.Registrations.Registration;

namespace Registration.Application.Common.Mappings;

/// <summary>
/// AutoMapper profile for domain -> DTO projection. Inbound mapping
/// (request -> domain) is done by hand in the command handler because building
/// value objects requires their factory methods and the injected submission date.
/// </summary>
public sealed class RegistrationMappingProfile : Profile
{
    public RegistrationMappingProfile()
    {
        CreateMap<DomainRegistration, RegistrationDetailsDto>()
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name.First))
            .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.Name.Middle))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name.Last))
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name.FullName))
            .ForMember(d => d.BirthDate, o => o.MapFrom(s => s.BirthDate.Value))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Value))
            .ForMember(d => d.Mobile, o => o.MapFrom(s => s.Mobile.Value))
            .ForMember(d => d.Addresses, o => o.MapFrom(s => s.Addresses));

        CreateMap<DomainAddress, AddressDetailsDto>()
            // Lookup names are enriched by the query handler after mapping.
            .ForMember(d => d.GovernorateName, o => o.Ignore())
            .ForMember(d => d.CityName, o => o.Ignore());

        CreateMap<DomainRegistration, RegistrationSummaryDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.Name.FullName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Value))
            .ForMember(d => d.Mobile, o => o.MapFrom(s => s.Mobile.Value))
            .ForMember(d => d.AddressCount, o => o.MapFrom(s => s.Addresses.Count));

        CreateMap<Governorate, GovernorateDto>();

        CreateMap<City, CityDto>();
    }
}
