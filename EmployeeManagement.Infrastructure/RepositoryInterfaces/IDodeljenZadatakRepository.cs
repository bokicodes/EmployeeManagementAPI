using EmployeeManagement.Infrastructure.Models;

namespace EmployeeManagement.Infrastructure.RepositoryInterfaces;

public interface IDodeljenZadatakRepository
{
    Task<IEnumerable<DodeljenZadatak>> VratiSveAsync(int zaposleniId);
    Task<DodeljenZadatak?> VratiPoIdsAsync(int zaposleniId, int zadatakId);
    Task<DodeljenZadatak> DodajAsync(DodeljenZadatak dodeljenZadatak);
    DodeljenZadatak Azuriraj(DodeljenZadatak dodeljenZadatak);
    Task ObrisiAsync(int zaposleniId, int zadatakId);
    Task SacuvajPromeneAsync();
}