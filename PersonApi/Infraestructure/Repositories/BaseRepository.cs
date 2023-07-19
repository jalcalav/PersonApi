using Microsoft.EntityFrameworkCore;
using PersonApi.Infraestructure.Interfaces;

namespace PersonApi.Infraestructure.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected DbContext _appDbContext;

    public BaseRepository(DbContext applicationDbContext)
    {
        _appDbContext = applicationDbContext;
    }

    public async Task<T?> FindByIdAsync<TId>(TId id)
    {
        return await _appDbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        _appDbContext.Set<T>().Add(entity);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _appDbContext.Entry(entity).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _appDbContext.Set<T>().Remove(entity);
        return await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}