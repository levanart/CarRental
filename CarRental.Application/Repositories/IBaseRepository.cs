using CarRental.Domain.Common;

namespace CarRental.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task Create(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Delete(T entity, CancellationToken cancellationToken);
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
}