using CarRental.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Persistence.Context;

public class CarRentalDbContext : DbContext
{
    public DbSet<Car>  Cars { get; set; }
    public DbSet<User>  Users  { get; set; }

    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
    {
        
    }
}