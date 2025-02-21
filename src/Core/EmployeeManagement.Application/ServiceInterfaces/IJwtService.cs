using EmployeeManagement.Application.DTOs.KorisnickiNalog;
using EmployeeManagement.Application.Identity;
using System.Security.Claims;

namespace EmployeeManagement.Application.ServiceInterfaces;

public interface IJwtService
{
    AutentikacioniOdgovor KreirajJwtToken(ApplicationUser user);
    ClaimsPrincipal? VratiInformacijeIzTokena(string? token);
}
