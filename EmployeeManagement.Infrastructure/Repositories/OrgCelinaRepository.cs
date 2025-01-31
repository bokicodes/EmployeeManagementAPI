using EmployeeManagement.Infrastructure.DatabaseContext;
using EmployeeManagement.Infrastructure.RepositoryInterfaces;
using EmployeeManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class OrgCelinaRepository : GenerickiRepository<OrganizacionaCelina>, IOrgCelinaRepository
{
    public OrgCelinaRepository(EmployeeManagementDBContext context) : base(context)
    {
    }

    public async Task<OrganizacionaCelina?> VratiOrgCelinuSaDetaljimaAsync(int orgCelinaId)
    {
        return await _context.OrganizacioneCeline.Where(oc => oc.OrgCelinaId == orgCelinaId)
            .Include(oc => oc.Zaposleni)
            .FirstOrDefaultAsync();
    }
}
