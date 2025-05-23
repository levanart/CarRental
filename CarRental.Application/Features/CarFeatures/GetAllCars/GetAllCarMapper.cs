using AutoMapper;
using CarRental.Application.Features.CarFeatures.GetAllCars;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.CarFeatures.GetAllCars;

public class GetAllCarMapper : Profile
{
    public GetAllCarMapper()
    {
        CreateMap<Car, GetAllCarResponse>();
    }
}