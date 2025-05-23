using CarRental.Domain.Common;

namespace CarRental.Domain.Entity;

public sealed class Car : BaseEntity
{
    public string PlateNumber { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Power { get; set; }
    public int Mileage { get; set; }
    public int ReleaseYear { get; set; }
    
    public ICollection<User> Users { get; set; } = new List<User>();
}