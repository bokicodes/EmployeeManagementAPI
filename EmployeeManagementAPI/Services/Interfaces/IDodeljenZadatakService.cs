using EmployeeManagementAPI.DTOs.DodeljenZadatak;
using EmployeeManagementAPI.DTOs.Zaposleni;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IDodeljenZadatakService
{
    Task<IEnumerable<DodeljenZadatakDTO>> GetAllDodeljeniZadaciAsync(int zaposleniId);
    Task<DodeljenZadatakDTO> GetDodeljenZadatakByIdsAsync(int zaposleniId, int zadatakId);
    Task<DodeljenZadatakDTO> AddDodeljenZadatakAsync(int zaposleniId, int zadatakId,
        AddDodeljenZadatakDTO addDodeljenZadatakDto);
    Task UpdateDodeljenZadatakAsync(int zaposleniId, int zadatakId, 
        UpdateDodeljenZadatakDTO updateDodeljenZadatakDto);
    Task DeleteDodeljenZadatakAsync(int zaposleniId, int zadatakId);
}
