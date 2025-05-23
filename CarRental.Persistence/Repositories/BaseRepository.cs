using CarRental.Application.Repositories;
using CarRental.Domain.Common;
using CarRental.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    
    protected readonly CarRentalDbContext _context;

    public BaseRepository(CarRentalDbContext context)
    {
        _context = context;
    }
    
    public void Create(T entity)
    {
        _context.Add(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        entity.DateDeleted = DateTime.UtcNow;
        entity.IsDeleted = true;
        _context.Remove(entity);
    }

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }
}