using EmployeeManagement.Business.DTOs.Zaposleni;

namespace EmployeeManagement.Business.ServiceInterfaces;

public interface IZaposleniService
{
    Task<IEnumerable<ZaposleniDTO>> VratiSveZaposleneAsync();
    Task<ZaposleniDTO> VratiZaposlenogPoIdAsync(int id);
    Task<ZaposleniDetaljnoDTO?> VratiZaposlenogSaDetaljimaAsync(int id);
    Task<ZaposleniDTO> DodajZaposlenogAsync(DodajZaposlenogDTO dodajZaposlenogDto);
    Task AzurirajZaposlenogAsync(AzurirajZaposlenogDTO azurirajZaposlenogDto);
    Task ObrisiZaposlenogAsync(int id);
}
