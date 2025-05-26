using MediatR;

namespace CarRental.Application.Features.UserFeatures.DeleteUser;

public sealed record DeleteUserRequest(
    Guid Id
    ) : IRequest<DeleteUserResponse>;