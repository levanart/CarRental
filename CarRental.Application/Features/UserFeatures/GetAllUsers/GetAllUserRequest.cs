using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetAllUsers;

public record GetAllUserRequest() : IRequest<List<GetAllUserResponse>>;