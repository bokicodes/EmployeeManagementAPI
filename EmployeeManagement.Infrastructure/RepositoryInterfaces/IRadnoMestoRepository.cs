using EmployeeManagement.Infrastructure.Models;

namespace EmployeeManagement.Infrastructure.RepositoryInterfaces;

public interface IRadnoMestoRepository : IGenerickiRepository<RadnoMesto>
{
    Task<RadnoMesto?> VratiRadnoMestoSaDetaljimaAsync(int radnoMestoId);
    Task<RadnoMesto?> VratiRadnoMestoPoZadatkuIdAsync(int zadatakId);
}
