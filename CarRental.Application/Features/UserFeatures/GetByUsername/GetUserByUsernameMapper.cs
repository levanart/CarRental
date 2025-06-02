using AutoMapper;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.GetByUsername;

public class GetUserByUsernameMapper : Profile
{
    public GetUserByUsernameMapper()
    {
        CreateMap<User, GetUserByUsernameResponse>();
    }
}