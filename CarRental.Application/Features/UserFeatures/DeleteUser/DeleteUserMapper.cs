using AutoMapper;
using CarRental.Application.Features.CarFeatures.DeleteCar;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.DeleteUser;

public class DeleteUserMapper : Profile
{
    public DeleteUserMapper()
    {
        CreateMap<User, DeleteUserResponse>();
    }
}