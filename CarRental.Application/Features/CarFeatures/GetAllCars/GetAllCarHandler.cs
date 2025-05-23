using AutoMapper;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarFeatures.GetAllCars;

public sealed class GetAllCarHandler : IRequestHandler<GetAllCarRequest, IEnumerable<GetAllCarResponse>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetAllCarHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllCarResponse>> Handle(
        GetAllCarRequest request, 
        CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<GetAllCarResponse>>(cars);
    }
}