using EmployeeManagementAPI.DTOs.RadnoMesto;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IRadnoMestoService
{
    Task<IEnumerable<RadnoMestoDTO>> GetAllRadnoMestoAsync();
    Task<RadnoMestoMoreInfoDTO?> GetRadnoMestoByIdAsync(int id);
    Task<RadnoMestoDTO> AddRadnoMestoAsync(AddRadnoMestoDTO addRadnoMestoDto);
    Task UpdateRadnoMestoAsync(UpdateRadnoMestoDTO updateRadnoMestoDto);
    Task DeleteRadnoMestoAsync(int id);
}
