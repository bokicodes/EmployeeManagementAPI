using AutoMapper;
using EmployeeManagementAPI.DTOs.DodeljenZadatak;
using EmployeeManagementAPI.DTOs.OrganizacionaCelina;
using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.TipZadatka;
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

        CreateMap<TipZadatka, TipZadatkaDTO>().ReverseMap();
        CreateMap<TipZadatka, AddTipZadatkaDTO>().ReverseMap();
        CreateMap<TipZadatka, UpdateTipZadatkaDTO>().ReverseMap();

        CreateMap<DodeljenZadatak, DodeljenZadatakDTO>()
            .ForMember(dz => dz.NazivZaposlenog, opt => opt.MapFrom(src => $"{src.Zaposleni.Ime} {src.Zaposleni.Prezime}"))
            .ForMember(dz => dz.NazivZadatka, opt => opt.MapFrom(src => src.TipZadatka.NazivZad))
            .ReverseMap();
        CreateMap<DodeljenZadatak, AddDodeljenZadatakDTO>().ReverseMap();
        CreateMap<DodeljenZadatak, UpdateDodeljenZadatakDTO>().ReverseMap();

    }
}
