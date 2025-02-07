using EmployeeManagement.Domain.RepositoryInterfaces;
using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Infrastructure.DatabaseContext;

namespace EmployeeManagement.Infrastructure.Repositories;

public class ZaposleniRepository : GenerickiRepository<Zaposleni>, IZaposleniRepository
{
    public ZaposleniRepository(EmployeeManagementDBContext context) : base(context)
    {
    }

    public async Task<Zaposleni?> VratiZaposlenogSaDetaljimaAsync(int zaposleniId)
    {
        return await _context.Zaposleni
            .Where(z => z.ZaposleniId == zaposleniId)
            .Include(z => z.RadnoMesto).Include(z => z.OrgCelina)
            .FirstOrDefaultAsync();
    }
}
