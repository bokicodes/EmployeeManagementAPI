using EmployeeManagementAPI.DTOs.Zaposleni;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IZaposleniService
{
    Task<IEnumerable<ZaposleniDTO>> GetAllZaposleniAsync();
    Task<ZaposleniDTO> GetZaposleniByIdAsync(int id);
    Task<ZaposleniMoreInfoDTO?> GetZaposleniWithAdditionalInfo(int id);
    Task<ZaposleniDTO> AddZaposleniAsync(AddZaposleniDTO addZaposleniDto);
    Task UpdateZaposleniAsync(UpdateZaposleniDTO updateZaposleniDto);
    Task DeleteZaposleniAsync(int id);
}
