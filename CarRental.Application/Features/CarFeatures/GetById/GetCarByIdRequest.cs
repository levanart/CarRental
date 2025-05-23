using CarRental.Application.Features.UserFeatures.GetAllUsers;
using MediatR;

namespace CarRental.Application.Features.CarFeatures.GetById;

public record GetCarByIdRequest(Guid Id) : IRequest<IEnumerable<GetCarByIdResponse>>;