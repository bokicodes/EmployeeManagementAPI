using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IZaposleniRepository : IGenericRepository<Zaposleni>
{
    Task<Zaposleni?> GetZaposleniWithAdditionalInfoAsync(int zaposleniId);

}
