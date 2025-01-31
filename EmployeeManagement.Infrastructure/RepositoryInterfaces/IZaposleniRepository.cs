using EmployeeManagement.Infrastructure.Models;

namespace EmployeeManagement.Infrastructure.RepositoryInterfaces;

public interface IZaposleniRepository : IGenerickiRepository<Zaposleni>
{
    Task<Zaposleni?> VratiZaposlenogSaDetaljimaAsync(int zaposleniId);

}
