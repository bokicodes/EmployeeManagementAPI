using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Repositories;

public class ZaposleniRepository : GenericRepository<Zaposleni>, IZaposleniRepository
{
    public ZaposleniRepository(EmployeeManagementDBContext context) : base(context)
    {
    }

    public void DeleteZaposleni(Zaposleni zaposleni)
    {
        throw new NotImplementedException();
    }
}
