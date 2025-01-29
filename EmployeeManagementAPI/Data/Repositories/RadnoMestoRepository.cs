using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data.Repositories;

public class RadnoMestoRepository : GenerickiRepository<RadnoMesto>, IRadnoMestoRepository
{
    public RadnoMestoRepository(EmployeeManagementDBContext context) : base(context)
    {
    }

    public async Task<RadnoMesto?> VratiRadnoMestoPoTipuZadatkaIdAsync(int zadatakId)
    {
        return await _context.RadnaMesta
            .Where(rm => rm.TipoviZadataka.Any(tz => tz.ZadatakId == zadatakId))
            .FirstOrDefaultAsync();
    }

    public async Task<RadnoMesto?> VratiRadnoMestoSaDetaljimaAsync(int radnoMestoId)
    {
        return await _context.RadnaMesta.Where(rm => rm.RadnoMestoId == radnoMestoId)
            .Include(rm => rm.Zaposleni).Include(rm => rm.TipoviZadataka)
            .FirstOrDefaultAsync();
    }
}
