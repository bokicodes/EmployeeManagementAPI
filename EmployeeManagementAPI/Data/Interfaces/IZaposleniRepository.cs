using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IZaposleniRepository : IGenerickiRepository<Zaposleni>
{
    Task<Zaposleni?> VratiZaposlenogSaDetaljimaAsync(int zaposleniId);

}
