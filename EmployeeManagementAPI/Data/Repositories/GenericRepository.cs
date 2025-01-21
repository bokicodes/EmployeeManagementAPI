using EmployeeManagementAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly EmployeeManagementDBContext _context;

    public GenericRepository(EmployeeManagementDBContext context)
    {
        _context = context;
    }


    public virtual async Task<T> AddAsync(T entity)
    {
        var entry = await _context.AddAsync(entity);

        return entry.Entity;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _context.FindAsync<T>(id);
    }

    public virtual T Update(T entity)
    {
        return _context.Update(entity).Entity;
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        _context.Set<T>().Remove(entity!);
    }
}
