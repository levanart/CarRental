using AutoMapper;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.CarFeatures.GetById;

public sealed class GetCarByIdHandler : IRequestHandler<GetCarByIdRequest, IEnumerable<GetCarByIdResponse>>
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public GetCarByIdHandler(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCarByIdResponse>> Handle(
        GetCarByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<GetCarByIdResponse>>(cars);
    }
}