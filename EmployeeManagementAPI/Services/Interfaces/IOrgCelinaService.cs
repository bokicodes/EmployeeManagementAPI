using EmployeeManagementAPI.DTOs.OrganizacionaCelina;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IOrgCelinaService
{
    Task<IEnumerable<OrgCelinaDTO>> VratiSveOrgCelineAsync();
    Task<OrgCelinaDetaljnoDTO?> VratiOrgCelinuPoIdAsync(int id);
    Task<OrgCelinaDTO> DodajOrgCelinuAsync(DodajOrgCelinuDTO dodajOrgCelinuDto);
    Task AzurirajOrgCelinuAsync(AzurirajOrgCelinuDTO azurirajOrgCelinuDto);
    Task ObrisiOrgCelinuAsync(int id);
}
