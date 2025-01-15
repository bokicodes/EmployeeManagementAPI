using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IZaposleniRepository : IGenericRepository<Zaposleni>
{
    void DeleteZaposleni(Zaposleni zaposleni);
}
