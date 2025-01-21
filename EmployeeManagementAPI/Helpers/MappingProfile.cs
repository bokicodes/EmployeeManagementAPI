using AutoMapper;
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
    }
}
