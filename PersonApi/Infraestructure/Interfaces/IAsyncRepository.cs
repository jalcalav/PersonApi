namespace PersonApi.Infraestructure.Interfaces;

public interface IAsyncRepository<T> where T : class
{
    Task<T?> FindByIdAsync<TId>(TId id);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<int> DeleteAsync(T entity, CancellationToken cancellationToken);
}
