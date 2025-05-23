using AutoMapper;
using CarRental.Application.Common.Exceptions;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;

namespace CarRental.Application.Features.CarFeatures.UpdateCar;

public class UpdateCarHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    
    public UpdateCarHandler(IUnitOfWork unitOfWork, ICarRepository carRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _carRepository = carRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateCarResponse> Handle(UpdateCarRequest request, CancellationToken cancellationToken)
    {
        var carToUpdate = await _carRepository.GetById(request.CarId,  cancellationToken);
        if (carToUpdate == null) throw new NotFoundException($"Car with id {request.CarId} not found");
        _mapper.Map(request, carToUpdate);
        carToUpdate.DateUpdated = DateTime.UtcNow;
        return _mapper.Map<UpdateCarResponse>(carToUpdate);
    }
}