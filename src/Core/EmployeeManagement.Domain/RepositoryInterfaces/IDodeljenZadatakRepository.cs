using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.RepositoryInterfaces;

public interface IDodeljenZadatakRepository
{
    Task<IEnumerable<DodeljenZadatak>> VratiSveAsync(int zaposleniId);
    Task<DodeljenZadatak?> VratiPoIdsAsync(int zaposleniId, int zadatakId);
    Task<DodeljenZadatak> DodajAsync(DodeljenZadatak dodeljenZadatak);
    DodeljenZadatak Azuriraj(DodeljenZadatak dodeljenZadatak);
    Task ObrisiAsync(int zaposleniId, int zadatakId);
    Task SacuvajPromeneAsync();
}