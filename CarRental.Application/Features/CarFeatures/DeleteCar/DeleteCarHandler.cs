using AutoMapper;
using CarRental.Application.Common.Exceptions;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using MediatR;

namespace CarRental.Application.Features.CarFeatures.DeleteCar;

public class DeleteCarHandler :  IRequestHandler<DeleteCarRequest, DeleteCarResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    
    public DeleteCarHandler(IUnitOfWork unitOfWork, ICarRepository carRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _carRepository = carRepository;
        _mapper = mapper;
    }
    
    public async Task<DeleteCarResponse> Handle(DeleteCarRequest request, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetById(request.Id, cancellationToken);
        if (car == null) throw new NotFoundException($"Car with Id: {request.Id} not found!");
        _carRepository.DeleteCar(car, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
        return _mapper.Map<DeleteCarResponse>(car);
    }
}