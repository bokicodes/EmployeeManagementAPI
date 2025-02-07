using EmployeeManagement.Application.DTOs.RadnoMesto;
using EmployeeManagement.Application.DTOs.Zadatak;

namespace EmployeeManagement.Application.ServiceInterfaces;

public interface IRadnoMestoService
{
    Task<IEnumerable<RadnoMestoDTO>> VratiSvaRadnaMestaAsync();
    Task<RadnoMestoDetaljnoDTO?> VratiRadnoMestoPoIdAsync(int id);
    Task<RadnoMestoDTO> VratiRadnoMestoPoZadatkuIdAsync(int zadatakId);
    Task<RadnoMestoDTO> DodajRadnoMestoAsync(DodajRadnoMestoDTO dodajRadnoMestoDto);
    Task AzurirajRadnoMestoAsync(AzurirajRadnoMestoDTO azurirajRadnoMestoDto);
    Task ObrisiRadnoMestoAsync(int id);
    Task DodajZadatakZaRadnoMestoAsync(int id, DodajZadatakDTO dodajZadatakDto);
    Task AzurirajZadatakZaRadnoMestoAsync(int id, int zadatakId, AzurirajZadatakDTO azurirajZadatakDto);
    Task ObrisiZadatakZaRadnoMestoAsync(int id, int zadatakId);
}
