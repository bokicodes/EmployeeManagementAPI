using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IRadnoMestoRepository : IGenericRepository<RadnoMesto>
{
    Task<RadnoMesto?> GetRadnoMestoWithAdditionalInfoAsync(int radnoMestoId);
    Task<RadnoMesto?> GetRadnoMestoByTipZadatkaIdAsync(int zadatakId);
}
