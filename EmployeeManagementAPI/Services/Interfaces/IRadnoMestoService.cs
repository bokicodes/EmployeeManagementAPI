using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.TipZadatka;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IRadnoMestoService
{
    Task<IEnumerable<RadnoMestoDTO>> GetAllRadnoMestoAsync();
    Task<RadnoMestoMoreInfoDTO?> GetRadnoMestoByIdAsync(int id);
    Task<RadnoMestoDTO> AddRadnoMestoAsync(AddRadnoMestoDTO addRadnoMestoDto);
    Task UpdateRadnoMestoAsync(UpdateRadnoMestoDTO updateRadnoMestoDto);
    Task DeleteRadnoMestoAsync(int id);
    Task AddTipZadatkaForRadnoMestoAsync(int id, AddTipZadatkaDTO tipZadatkaDTO);
    Task UpdateTipZadatkaForRadnoMestoAsync(int id, int tipZadatkaId, UpdateTipZadatkaDTO updateTipZadatkaDTO);
    Task DeleteTipZadatkaForRadnoMestoAsync(int id,  int tipZadatkaId);
}
