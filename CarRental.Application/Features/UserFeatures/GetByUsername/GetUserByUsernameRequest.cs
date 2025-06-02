using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetByUsername;

public record GetUserByUsernameRequest(string Username) : IRequest<GetUserByUsernameResponse>;