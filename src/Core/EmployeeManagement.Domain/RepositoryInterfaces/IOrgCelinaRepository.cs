using EmployeeManagement.Domain.Models;

namespace EmployeeManagement.Domain.RepositoryInterfaces;

public interface IOrgCelinaRepository : IGenerickiRepository<OrganizacionaCelina>
{
    Task<OrganizacionaCelina?> VratiOrgCelinuSaDetaljimaAsync(int orgCelinaId);
}