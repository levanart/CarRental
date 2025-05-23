using AutoMapper;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.CarFeatures.CreateCar;

public class CreateCarMapper : Profile
{
    public CreateCarMapper()
    {
        CreateMap<CreateCarRequest, Car>();
        CreateMap<Car, CreateCarResponse>();
    }
}