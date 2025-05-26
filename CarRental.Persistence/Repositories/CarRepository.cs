using CarRental.Application.Features.CarFeatures.DeleteCar;
using CarRental.Application.Features.CarFeatures.UpdateCar;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using CarRental.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Persistence.Repositories;

public class CarRepository : BaseRepository<Car>, ICarRepository
{
    public CarRepository(CarRentalDbContext context) : base(context)
    {
    }

    public async Task<List<Car>> GetByBrandAdnModel(string brand, string model, CancellationToken cancellationToken)
    {
        return await _context.Cars.Where(c => brand.Equals(c.Brand) && model.Equals(c.Model)).ToListAsync();
    }

    public async Task<Car?> GetByPlate(string plateNumber, CancellationToken cancellationToken)
    {
        return await _context.Cars.FirstOrDefaultAsync(c => c.PlateNumber.Equals(plateNumber));
    }
    
    public async Task<Car?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _context.Cars.FirstOrDefaultAsync(c => c.Id.Equals(id));
    }

    public async Task<List<Car>> GetGreaterReleaseYear(int releaseYear, CancellationToken cancellationToken)
    {
        return await _context.Cars.Where(c => c.ReleaseYear >= releaseYear).ToListAsync();
    }

    public Car DeleteCar(Car carToDelete, CancellationToken cancellationToken)
    {
        _context.Cars.Remove(carToDelete);
        return carToDelete;
    }
}