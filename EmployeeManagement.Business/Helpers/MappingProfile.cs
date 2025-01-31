using AutoMapper;
using EmployeeManagement.Business.DTOs.DodeljenZadatak;
using EmployeeManagement.Business.DTOs.OrganizacionaCelina;
using EmployeeManagement.Business.DTOs.RadnoMesto;
using EmployeeManagement.Business.DTOs.TipZadatka;
using EmployeeManagement.Business.DTOs.Zaposleni;
using EmployeeManagement.Infrastructure.Models;

namespace EmployeeManagement.Business.Helpers;

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

        CreateMap<TipZadatka, TipZadatkaDTO>().ReverseMap();
        CreateMap<TipZadatka, DodajTipZadatkaDTO>().ReverseMap();
        CreateMap<TipZadatka, AzurirajTipZadatkaDTO>().ReverseMap();

        CreateMap<DodeljenZadatak, DodeljenZadatakDTO>()
            .ForMember(dz => dz.NazivZaposlenog, opt => opt.MapFrom(src => $"{src.Zaposleni.Ime} {src.Zaposleni.Prezime}"))
            .ForMember(dz => dz.NazivZadatka, opt => opt.MapFrom(src => src.TipZadatka.NazivZad))
            .ReverseMap();
        CreateMap<DodeljenZadatak, DodeliZadatakDTO>().ReverseMap();
        CreateMap<DodeljenZadatak, AzurirajDodeljenZadatakDTO>().ReverseMap();

    }
}
