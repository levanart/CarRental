using AutoMapper;
using CarRental.Application.Features.CarFeatures.CreateCar;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.CarFeatures.DeleteCar;

public class DeleteCarMapper : Profile
{
    public DeleteCarMapper()
    {
        CreateMap<Car, DeleteCarResponse>();
    }
}