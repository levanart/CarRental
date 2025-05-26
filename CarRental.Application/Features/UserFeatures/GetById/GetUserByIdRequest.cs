using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetById;

public record GetUserByIdRequest(Guid Id) : IRequest<GetUserByIdResponse>;