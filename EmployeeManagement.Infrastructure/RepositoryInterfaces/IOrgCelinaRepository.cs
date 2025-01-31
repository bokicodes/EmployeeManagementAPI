using EmployeeManagement.Infrastructure.Models;

namespace EmployeeManagement.Infrastructure.RepositoryInterfaces;

public interface IOrgCelinaRepository : IGenerickiRepository<OrganizacionaCelina>
{
    Task<OrganizacionaCelina?> VratiOrgCelinuSaDetaljimaAsync(int orgCelinaId);
}