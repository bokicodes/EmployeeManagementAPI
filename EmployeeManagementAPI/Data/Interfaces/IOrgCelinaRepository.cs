using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IOrgCelinaRepository : IGenerickiRepository<OrganizacionaCelina>
{
    Task<OrganizacionaCelina?> VratiOrgCelinuSaDetaljimaAsync(int orgCelinaId);
}