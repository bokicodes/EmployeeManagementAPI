using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data.Repositories;

public class DodeljenZadatakRepository : IDodeljenZadatakRepository
{
    private readonly EmployeeManagementDBContext _context;

    public DodeljenZadatakRepository(EmployeeManagementDBContext context)
    {
        _context = context;
    }

    public async Task<DodeljenZadatak> AddAsync(DodeljenZadatak dodeljenZadatak)
    {
        var entity = await _context.DodeljeniZadaci.AddAsync(dodeljenZadatak);
        return entity.Entity;
    }

    public async Task DeleteAsync(int zaposleniId, int zadatakId)
    {
        var dodeljenZadatak = await GetByIdsAsync(zaposleniId, zadatakId);
        _context.DodeljeniZadaci.Remove(dodeljenZadatak!);
    }

    public async Task<IEnumerable<DodeljenZadatak>> GetAllAsync(int zaposleniId)
    {
        return await _context.DodeljeniZadaci.Where(dz => dz.ZaposleniId == zaposleniId)
            .Include(dz => dz.TipZadatka)
            .ToListAsync();    
    }

    public async Task<DodeljenZadatak?> GetByIdsAsync(int zaposleniId, int zadatakId)
    {
        return await _context.DodeljeniZadaci.Include(dz => dz.TipZadatka)
            .FirstOrDefaultAsync(dz => dz.ZaposleniId == zaposleniId
                && dz.ZadatakId == zadatakId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();  
    }

    public DodeljenZadatak Update(DodeljenZadatak dodeljenZadatak)
    {
        var entity = _context.DodeljeniZadaci.Update(dodeljenZadatak);
        return entity.Entity;
    }
}
