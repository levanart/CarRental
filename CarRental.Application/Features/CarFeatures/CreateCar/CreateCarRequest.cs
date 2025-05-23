using MediatR;

namespace CarRental.Application.Features.CarFeatures.CreateCar;

public sealed record CreateCarRequest(
    string PlateNumber,
    string Brand,
    string Model,
    int Power,
    int Mileage,
    int ReleaseYear
    ) : IRequest<CreateCarResponse>;