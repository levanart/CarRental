using AutoMapper;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.GetAllUsers;

public class GetAllUserMapper : Profile
{
    public GetAllUserMapper()
    {
        CreateMap<User, GetAllUserResponse>();
    }
}