namespace Swimmer.Infrastructure.Persistance;

using Domain.Entities;
using Services;

public class EfRepository<T> : IRepository<T>
    where T : BaseEntity
{
    public Task<T> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task Save(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll()
    {
        throw new NotImplementedException();
    }
}