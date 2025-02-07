using EmployeeManagement.Infrastructure.DatabaseContext;
using EmployeeManagement.Infrastructure.RepositoryInterfaces;
using EmployeeManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class RadnoMestoRepository : GenerickiRepository<RadnoMesto>, IRadnoMestoRepository
{
    public RadnoMestoRepository(EmployeeManagementDBContext context) : base(context)
    {
    }

    public async Task<RadnoMesto?> VratiRadnoMestoPoZadatkuIdAsync(int zadatakId)
    {
        return await _context.RadnaMesta
            .Where(rm => rm.Zadaci.Any(tz => tz.ZadatakId == zadatakId))
            .FirstOrDefaultAsync();
    }

    public async Task<RadnoMesto?> VratiRadnoMestoSaDetaljimaAsync(int radnoMestoId)
    {
        return await _context.RadnaMesta.Where(rm => rm.RadnoMestoId == radnoMestoId)
            .Include(rm => rm.Zaposleni).Include(rm => rm.Zadaci)
            .FirstOrDefaultAsync();
    }
}
