using CarRental.Domain.Entity;

namespace CarRental.Application.Repositories;

public interface ICarRepository : IBaseRepository<Car>
{
    Task<List<Car>> GetByBrandAdnModel(string brand,
        string model,
        CancellationToken cancellationToken);
    
    Task<Car?> GetByPlate(string plateNumber,
        CancellationToken cancellationToken);
    
    Task<List<Car>> GetGreaterReleaseYear(int releaseYear,
        CancellationToken cancellationToken);
}