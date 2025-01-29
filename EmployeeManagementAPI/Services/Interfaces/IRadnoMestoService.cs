using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.TipZadatka;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IRadnoMestoService
{
    Task<IEnumerable<RadnoMestoDTO>> VratiSvaRadnaMestaAsync();
    Task<RadnoMestoDetaljnoDTO?> VratiRadnoMestoPoIdAsync(int id);
    Task<RadnoMestoDTO> VratiRadnoMestoPoTipuZadatkaIdAsync(int zadatakId);
    Task<RadnoMestoDTO> DodajRadnoMestoAsync(DodajRadnoMestoDTO dodajRadnoMestoDto);
    Task AzurirajRadnoMestoAsync(AzurirajRadnoMestoDTO azurirajRadnoMestoDto);
    Task ObrisiRadnoMestoAsync(int id);
    Task DodajTipZadatkaZaRadnoMestoAsync(int id, DodajTipZadatkaDTO dodajTipZadatkaDTO);
    Task AzurirajTipZadatkaZaRadnoMestoAsync(int id, int tipZadatkaId, AzurirajTipZadatkaDTO azurirajTipZadatkaDTO);
    Task ObrisiTipZadatkaZaRadnoMestoAsync(int id,  int tipZadatkaId);
}
