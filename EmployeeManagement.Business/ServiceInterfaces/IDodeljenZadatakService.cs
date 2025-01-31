using EmployeeManagement.Business.DTOs.DodeljenZadatak;

namespace EmployeeManagement.Business.ServiceInterfaces;

public interface IDodeljenZadatakService
{
    Task<IEnumerable<DodeljenZadatakDTO>> VratiSveDodeljeneZadatkeAsync(int zaposleniId);
    Task<DodeljenZadatakDTO> VratiDodeljenZadatakPoIdsAsync(int zaposleniId, int zadatakId);
    Task<DodeljenZadatakDTO> DodeliZadatakAsync(int zaposleniId, int zadatakId,
        DodeliZadatakDTO dodeliZadatakDto);
    Task AzurirajDodeljenZadatakAsync(int zaposleniId, int zadatakId,
        AzurirajDodeljenZadatakDTO azurirajDodeljenZadatakDto);
    Task ObrisiDodeljenZadatakAsync(int zaposleniId, int zadatakId);
}
