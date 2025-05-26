using AutoMapper;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.CarFeatures.UpdateCar;

public class UpdateCarMapper : Profile
{
    public UpdateCarMapper()
    {
        CreateMap<UpdateCarRequest, Car>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Car, UpdateCarResponse>();
    }
}