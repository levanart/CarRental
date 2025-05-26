using AutoMapper;
using CarRental.Application.Features.CarFeatures.UpdateCar;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.UpdateUser;

public class UpdateUserMapper : Profile
{
    public UpdateUserMapper()
    {
        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));


        CreateMap<User, UpdateUserResponse>();
    }
}