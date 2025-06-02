using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetByPhone;

public record GetUserByPhoneRequest(string PhoneNumber) : IRequest<GetUserByPhoneResponse>;