using AutoMapper;
using EmployeeManagement.Application.DTOs.DodeljenZadatak;
using EmployeeManagement.Application.DTOs.OrganizacionaCelina;
using EmployeeManagement.Application.DTOs.RadnoMesto;
using EmployeeManagement.Application.DTOs.Zadatak;
using EmployeeManagement.Application.DTOs.Zaposleni;
using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Application.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Zaposleni, ZaposleniDTO>().ReverseMap();
        CreateMap<Zaposleni, ZaposleniDetaljnoDTO>()
            .ForMember(dest => dest.RadnoMesto, opt => opt.MapFrom(src => src.RadnoMesto.NazivRM))
            .ForMember(dest => dest.OrganizacionaCelina, opt => opt
            .MapFrom(src => src.OrgCelina != null ? src.OrgCelina.NazivOC : ""))
            .ReverseMap();
        CreateMap<Zaposleni, DodajZaposlenogDTO>().ReverseMap();
        CreateMap<Zaposleni, AzurirajZaposlenogDTO>().ReverseMap();

        CreateMap<RadnoMesto, RadnoMestoDTO>().ReverseMap();
        CreateMap<RadnoMesto, RadnoMestoDetaljnoDTO>().ReverseMap();
        CreateMap<RadnoMesto, DodajRadnoMestoDTO>().ReverseMap();
        CreateMap<RadnoMesto, AzurirajRadnoMestoDTO>().ReverseMap();

        CreateMap<OrganizacionaCelina, OrgCelinaDTO>().ReverseMap();
        CreateMap<OrganizacionaCelina, OrgCelinaDetaljnoDTO>().ReverseMap();
        CreateMap<OrganizacionaCelina, DodajOrgCelinuDTO>().ReverseMap();
        CreateMap<OrganizacionaCelina, AzurirajOrgCelinuDTO>().ReverseMap();

        CreateMap<Zadatak, ZadatakDTO>()
            .ForMember(dest => dest.TipZadatka, opt => opt.MapFrom(src => src.TipZadatka))
            .ReverseMap();
        CreateMap<Zadatak, DodajZadatakDTO>()
            .ForMember(dest => dest.TipZadatka, opt => opt.MapFrom(src => src.TipZadatka))
            .ReverseMap();
        CreateMap<Zadatak, AzurirajZadatakDTO>()
            .ForMember(dest => dest.TipZadatka, opt => opt.MapFrom(src => src.TipZadatka))
            .ReverseMap();

        CreateMap<DodeljenZadatak, DodeljenZadatakDTO>()
            .ForMember(dz => dz.NazivZaposlenog, opt => opt.MapFrom(src => $"{src.Zaposleni.Ime} {src.Zaposleni.Prezime}"))
            .ForMember(dz => dz.NazivZadatka, opt => opt.MapFrom(src => src.Zadatak.NazivZad))
            .ReverseMap();
        CreateMap<DodeljenZadatak, DodeliZadatakDTO>().ReverseMap();
        CreateMap<DodeljenZadatak, AzurirajDodeljenZadatakDTO>().ReverseMap();

    }
}
