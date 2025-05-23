using CarRental.Application.Features.UserFeatures.GetAllUsers;
using MediatR;

namespace CarRental.Application.Features.CarFeatures.GetAllCars;

public record GetAllCarRequest() : IRequest<List<GetAllCarResponse>>;