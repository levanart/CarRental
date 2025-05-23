using CarRental.Application.Repositories;
using CarRental.Persistence.Context;

namespace CarRental.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarRentalDbContext _context;

    public UnitOfWork(CarRentalDbContext context)
    {
        _context = context;
    }


    public async Task Save(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}