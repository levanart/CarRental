using MediatR;

namespace CarRental.Application.Features.UserFeatures.CreateUser;

public sealed record CreateUserRequest(
    string Username,
    string Password,
    string Email,
    string PhoneNumber) : IRequest<CreateUserResponse>;