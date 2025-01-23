using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data.Repositories;

public class OrgCelinaRepository : GenericRepository<OrganizacionaCelina>, IOrgCelinaRepository
{
    public OrgCelinaRepository(EmployeeManagementDBContext context) : base(context)
    {
    }

    public async Task<OrganizacionaCelina?> GetOrgCelinaWithAdditionalInfoAsync(int orgCelinaId)
    {
        return await _context.OrganizacioneCeline.Where(oc => oc.OrgCelinaId == orgCelinaId)
            .Include(oc => oc.Zaposleni)
            .FirstOrDefaultAsync();
    }
}
