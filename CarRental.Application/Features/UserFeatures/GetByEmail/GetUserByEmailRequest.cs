using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetByEmail;

public record GetUserByEmailRequest(string Email) : IRequest<GetUserByEmailResponse>;