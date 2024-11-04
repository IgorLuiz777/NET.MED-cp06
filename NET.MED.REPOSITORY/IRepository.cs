using MongoDB.Bson;

namespace NET.MED.REPOSITORY;

public interface IRepository<T>
{
    Task<T?> GetById(Guid id);
    Task<List<T>> GetAll();
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(Guid id);
}