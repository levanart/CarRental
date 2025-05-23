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

    public async Task<List<Car>> GetGreaterReleaseYear(int releaseYear, CancellationToken cancellationToken)
    {
        return await _context.Cars.Where(c => c.ReleaseYear >= releaseYear).ToListAsync();
    }

    public async Task<Car?> UpdateCar(UpdateCarRequest newData, CancellationToken cancellationToken)
    {
        var carToUpdate = await _context.Cars.FirstOrDefaultAsync(c => c.Id == newData.CarId);
        if (carToUpdate == null) return null;
        
        carToUpdate.Brand = newData.Brand == null ? carToUpdate.Brand : newData.Brand;
        carToUpdate.Model = newData.Model == null ? carToUpdate.Model : newData.Model;
        carToUpdate.Power = newData.Power == 0 ? carToUpdate.Power : newData.Power;
        carToUpdate.Mileage =  newData.Mileage == 0 ? carToUpdate.Mileage : newData.Mileage;
        carToUpdate.ReleaseYear = newData.ReleaseYear == 0 ? carToUpdate.ReleaseYear : newData.ReleaseYear;
        carToUpdate.PlateNumber = newData.PlateNumber == null ? carToUpdate.PlateNumber : newData.PlateNumber;
        
        await _context.SaveChangesAsync();
        return carToUpdate;
    }
}