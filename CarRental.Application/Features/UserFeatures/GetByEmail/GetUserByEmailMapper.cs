using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetById;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.UserFeatures.GetByEmail;

public class GetUserByEmailMapper : Profile
{
    public GetUserByEmailMapper()
    {
        CreateMap<User, GetUserByEmailResponse>();
    }
}