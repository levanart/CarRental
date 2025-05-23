using AutoMapper;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarFeatures.GetById;

public sealed class GetCarByIdHandler : IRequestHandler<GetCarByIdRequest, GetCarByIdResponse>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarByIdHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<GetCarByIdResponse> Handle(
        GetCarByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetById(request.Id,  cancellationToken);
        return _mapper.Map<GetCarByIdResponse>(cars);
    }
}