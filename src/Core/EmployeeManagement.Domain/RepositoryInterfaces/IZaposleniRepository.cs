using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.RepositoryInterfaces;

public interface IZaposleniRepository : IGenerickiRepository<Zaposleni>
{
    Task<Zaposleni?> VratiZaposlenogSaDetaljimaAsync(int zaposleniId);

}
