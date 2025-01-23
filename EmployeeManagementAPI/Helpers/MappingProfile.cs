using AutoMapper;
using EmployeeManagementAPI.DTOs.OrganizacionaCelina;
using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Zaposleni, ZaposleniDTO>().ReverseMap();
        CreateMap<Zaposleni, ZaposleniMoreInfoDTO>()
            .ForMember(dest => dest.RadnoMesto, opt => opt.MapFrom(src => src.RadnoMesto.NazivRM))
            .ForMember(dest => dest.OrganizacionaCelina, opt => opt
            .MapFrom(src => src.OrgCelina != null ? src.OrgCelina.NazivOC : ""))
            .ReverseMap();
        CreateMap<Zaposleni, AddZaposleniDTO>().ReverseMap();
        CreateMap<Zaposleni, UpdateZaposleniDTO>().ReverseMap();

        CreateMap<RadnoMesto, RadnoMestoDTO>().ReverseMap();
        CreateMap<RadnoMesto, RadnoMestoMoreInfoDTO>().ReverseMap();
        CreateMap<RadnoMesto, AddRadnoMestoDTO>().ReverseMap();
        CreateMap<RadnoMesto, UpdateRadnoMestoDTO>().ReverseMap();

        CreateMap<OrganizacionaCelina, OrgCelinaDTO>().ReverseMap();
        CreateMap<OrganizacionaCelina, OrgCelinaMoreInfoDTO>().ReverseMap();
        CreateMap<OrganizacionaCelina, AddOrgCelinaDTO>().ReverseMap();
        CreateMap<OrganizacionaCelina, UpdateOrgCelinaDTO>().ReverseMap();

    }
}
