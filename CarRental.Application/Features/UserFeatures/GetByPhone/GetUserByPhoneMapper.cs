using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetByEmail;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.GetByPhone;

public class GetUserByPhoneMapper : Profile
{
    public GetUserByPhoneMapper()
    {
        CreateMap<User, GetUserByPhoneResponse>();
    }
}