using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IDodeljenZadatakRepository
{
    Task<IEnumerable<DodeljenZadatak>> GetAllAsync(int zaposleniId);
    Task<DodeljenZadatak?> GetByIdsAsync(int zaposleniId, int zadatakId);
    Task<DodeljenZadatak> AddAsync(DodeljenZadatak dodeljenZadatak);
    DodeljenZadatak Update(DodeljenZadatak dodeljenZadatak);
    Task DeleteAsync(int zaposleniId, int zadatakId);
    Task SaveChangesAsync();
}
