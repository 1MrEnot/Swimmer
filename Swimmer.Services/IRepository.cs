namespace Swimmer.Services;

using Domain.Entities;

public interface IRepository<T>
    where T: BaseEntity
{
    Task<T> Get(int id);

    Task Save(T entity);

    Task Remove(T entity);

    IQueryable<T> GetAll();
}