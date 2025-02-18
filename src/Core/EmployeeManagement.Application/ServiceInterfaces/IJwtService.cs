using EmployeeManagement.Application.DTOs.KorisnickiNalog;
using EmployeeManagement.Application.Identity;

namespace EmployeeManagement.Application.ServiceInterfaces;

public interface IJwtService
{
    AutentikacioniOdgovor KreirajJwtToken(ApplicationUser user);
}
