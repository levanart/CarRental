namespace CarRental.Application.Features.CarFeatures.GetAllCars;

public record GetAllCarResponse
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Power { get; set; }
    public int Mileage { get; set; }
    public int ReleaseYear { get; set; }
}