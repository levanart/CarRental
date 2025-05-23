using AutoMapper;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using MediatR;

namespace CarRental.Application.Features.CarFeatures.CreateCar;

public class CreateCarHandler :  IRequestHandler<CreateCarRequest, CreateCarResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    
    public CreateCarHandler(IUnitOfWork unitOfWork, ICarRepository carRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _carRepository = carRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateCarResponse> Handle(CreateCarRequest request, CancellationToken cancellationToken)
    {
        var car = _mapper.Map<Car>(request);
        _carRepository.Create(car);
        await _unitOfWork.Save(cancellationToken);
        return _mapper.Map<CreateCarResponse>(car);
    }
}