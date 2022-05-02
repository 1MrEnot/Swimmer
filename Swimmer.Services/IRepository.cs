namespace Swimmer.Services;

using Domain.Entities;

public interface IRepository<T>
    where T: BaseEntity
{
    ValueTask<T?> GetOrDefault(int id);

    ValueTask<T> Get(int id);

    ValueTask Save(T entity);

    ValueTask Remove(T entity);

    IQueryable<T> GetAll();

    ValueTask SaveChanges();
}