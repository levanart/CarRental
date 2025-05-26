using AutoMapper;
using CarRental.Application.Features.CarFeatures.GetById;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.GetById;

public class GetUserByIdMapper : Profile
{
    public GetUserByIdMapper()
    {
        CreateMap<User, GetUserByIdResponse>();
    }
}