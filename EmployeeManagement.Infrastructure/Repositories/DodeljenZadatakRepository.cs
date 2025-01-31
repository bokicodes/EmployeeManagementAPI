using EmployeeManagement.Infrastructure.DatabaseContext;
using EmployeeManagement.Infrastructure.RepositoryInterfaces;
using EmployeeManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class DodeljenZadatakRepository : IDodeljenZadatakRepository
{
    private readonly EmployeeManagementDBContext _context;

    public DodeljenZadatakRepository(EmployeeManagementDBContext context)
    {
        _context = context;
    }

    public async Task<DodeljenZadatak> DodajAsync(DodeljenZadatak dodeljenZadatak)
    {
        var entity = await _context.DodeljeniZadaci.AddAsync(dodeljenZadatak);
        return entity.Entity;
    }

    public async Task ObrisiAsync(int zaposleniId, int zadatakId)
    {
        var dodeljenZadatak = await VratiPoIdsAsync(zaposleniId, zadatakId);
        _context.DodeljeniZadaci.Remove(dodeljenZadatak!);
    }

    public async Task<IEnumerable<DodeljenZadatak>> VratiSveAsync(int zaposleniId)
    {
        return await _context.DodeljeniZadaci.Where(dz => dz.ZaposleniId == zaposleniId)
            .Include(dz => dz.TipZadatka)
            .ToListAsync();
    }

    public async Task<DodeljenZadatak?> VratiPoIdsAsync(int zaposleniId, int zadatakId)
    {
        return await _context.DodeljeniZadaci.Include(dz => dz.TipZadatka)
            .FirstOrDefaultAsync(dz => dz.ZaposleniId == zaposleniId
                && dz.ZadatakId == zadatakId);
    }

    public async Task SacuvajPromeneAsync()
    {
        await _context.SaveChangesAsync();
    }

    public DodeljenZadatak Azuriraj(DodeljenZadatak dodeljenZadatak)
    {
        var entity = _context.DodeljeniZadaci.Update(dodeljenZadatak);
        return entity.Entity;
    }
}
