using AutoMapper;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserMapper : Profile
{
    public CreateUserMapper()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, CreateUserResponse>();
    }
}