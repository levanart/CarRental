using AutoMapper;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.CarFeatures.GetById;

public class GetCarByIdMapper : Profile
{
    public GetCarByIdMapper()
    {
        CreateMap<GetCarByIdRequest, Car>();
        CreateMap<Car, GetCarByIdResponse>();
    }
}