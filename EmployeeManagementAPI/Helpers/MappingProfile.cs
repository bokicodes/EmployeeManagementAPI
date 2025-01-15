using AutoMapper;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Zaposleni, ZaposleniDTO>();
    }
}
