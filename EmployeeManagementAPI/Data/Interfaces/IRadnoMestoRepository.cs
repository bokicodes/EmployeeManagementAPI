using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IRadnoMestoRepository : IGenerickiRepository<RadnoMesto>
{
    Task<RadnoMesto?> VratiRadnoMestoSaDetaljimaAsync(int radnoMestoId);
    Task<RadnoMesto?> VratiRadnoMestoPoTipuZadatkaIdAsync(int zadatakId);
}
