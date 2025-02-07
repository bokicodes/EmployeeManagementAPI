namespace EmployeeManagement.Domain.RepositoryInterfaces;

public interface IGenerickiRepository<T> where T : class
{
    Task<IEnumerable<T>> VratiSveAsync();
    Task<T?> VratiPoIdAsync(int id);
    Task<T> DodajAsync(T entity);
    T Azuriraj(T entity);
    Task ObrisiAsync(int id);
    Task SacuvajPromeneAsync();
}