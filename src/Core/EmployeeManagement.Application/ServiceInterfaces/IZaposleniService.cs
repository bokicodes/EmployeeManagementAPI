using EmployeeManagement.Application.DTOs.Zaposleni;

namespace EmployeeManagement.Application.ServiceInterfaces;

public interface IZaposleniService
{
    Task<IEnumerable<ZaposleniDTO>> VratiSveZaposleneAsync();
    Task<ZaposleniDTO> VratiZaposlenogPoIdAsync(int id);
    Task<ZaposleniDetaljnoDTO?> VratiZaposlenogSaDetaljimaAsync(int id);
    Task<ZaposleniDTO> DodajZaposlenogAsync(DodajZaposlenogDTO dodajZaposlenogDto);
    Task AzurirajZaposlenogAsync(AzurirajZaposlenogDTO azurirajZaposlenogDto);
    Task ObrisiZaposlenogAsync(int id);
}
