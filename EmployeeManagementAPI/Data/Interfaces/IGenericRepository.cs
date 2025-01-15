namespace EmployeeManagementAPI.Data.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    T Update(T entity);
    Task SaveChangesAsync();
}
