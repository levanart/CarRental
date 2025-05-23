using CarRental.Domain.Common;

namespace CarRental.Domain.Entity;

public sealed class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    
    public ICollection<Car> Cars { get; set; } = new List<Car>();
}