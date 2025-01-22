using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data.Repositories;

public class RadnoMestoRepository : GenericRepository<RadnoMesto>, IRadnoMestoRepository
{
    public RadnoMestoRepository(EmployeeManagementDBContext context) : base(context)
    {
    }

    public async Task<RadnoMesto?> GetRadnoMestoWithAdditionalInfoAsync(int radnoMestoId)
    {
        return await _context.RadnaMesta.Where(rm => rm.RadnoMestoId == radnoMestoId)
            .Include(rm => rm.Zaposleni).Include(rm => rm.TipoviZadataka)
            .FirstOrDefaultAsync();
    }
}
