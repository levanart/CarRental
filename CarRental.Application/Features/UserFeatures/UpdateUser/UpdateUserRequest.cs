using MediatR;

namespace CarRental.Application.Features.UserFeatures.UpdateUser;

public sealed record UpdateUserRequest(
    Guid UserId,
    string? Username,
    string? Email,
    string? Password,
    string? PhoneNumber
) : IRequest<UpdateUserResponse>;