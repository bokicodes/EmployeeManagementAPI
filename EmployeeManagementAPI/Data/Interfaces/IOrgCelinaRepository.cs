using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data.Interfaces;

public interface IOrgCelinaRepository : IGenericRepository<OrganizacionaCelina>
{
    Task<OrganizacionaCelina?> GetOrgCelinaWithAdditionalInfoAsync(int orgCelinaId);
}
