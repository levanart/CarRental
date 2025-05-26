using MediatR;

namespace CarRental.Application.Features.CarFeatures.DeleteCar;

public sealed record DeleteCarRequest(
    Guid Id
    ) : IRequest<DeleteCarResponse>;