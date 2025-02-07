using EmployeeManagement.Domain.RepositoryInterfaces;
using EmployeeManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Infrastructure.DatabaseContext;

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
