using MediatR;

namespace CarRental.Application.Features.CarFeatures.UpdateCar;

public sealed record UpdateCarRequest(
    Guid CarId,
    string? PlateNumber,
    string? Brand,
    string? Model,
    int Power,
    int Mileage,
    int ReleaseYear
    ) : IRequest<UpdateCarResponse>;