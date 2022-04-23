namespace Swimmer.Infrastructure.Persistance;

using Domain.Entities;
using Services;

public class EfRepository<T> : IRepository<T>
    where T : BaseEntity
{
    private readonly SwimmerContext _context;

    public EfRepository(SwimmerContext context)
    {
        _context = context;
    }

    public ValueTask<T?> Get(int id)
    {
        return _context.FindAsync<T>(id);
    }

    public async ValueTask Save(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async ValueTask Remove(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }
}