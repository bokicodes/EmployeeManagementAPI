using EmployeeManagement.Domain.RepositoryInterfaces;
using EmployeeManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;

public class GenerickiRepository<T> : IGenerickiRepository<T> where T : class
{
    protected readonly EmployeeManagementDBContext _context;

    public GenerickiRepository(EmployeeManagementDBContext context)
    {
        _context = context;
    }


    public virtual async Task<T> DodajAsync(T entity)
    {
        var entry = await _context.AddAsync(entity);

        return entry.Entity;
    }

    public virtual async Task<IEnumerable<T>> VratiSveAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> VratiPoIdAsync(int id)
    {
        return await _context.FindAsync<T>(id);
    }

    public virtual T Azuriraj(T entity)
    {
        return _context.Update(entity).Entity;
    }

    public virtual async Task SacuvajPromeneAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task ObrisiAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        _context.Set<T>().Remove(entity!);
    }
}
