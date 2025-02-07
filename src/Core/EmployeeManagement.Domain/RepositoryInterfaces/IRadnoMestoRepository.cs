using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.RepositoryInterfaces;

public interface IRadnoMestoRepository : IGenerickiRepository<RadnoMesto>
{
    Task<RadnoMesto?> VratiRadnoMestoSaDetaljimaAsync(int radnoMestoId);
    Task<RadnoMesto?> VratiRadnoMestoPoZadatkuIdAsync(int zadatakId);
}
