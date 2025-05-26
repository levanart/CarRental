using AutoMapper;
using CarRental.Application.Common.Exceptions;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.UserFeatures.UpdateUser;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    
    public UpdateUserHandler(IUnitOfWork unitOfWork, ICarRepository carRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _carRepository = carRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var carToUpdate = await _carRepository.GetById(request.UserId,  cancellationToken);
        if (carToUpdate == null) throw new NotFoundException($"Car with id {request.UserId} not found");
        _mapper.Map(request, carToUpdate);
        carToUpdate.DateUpdated = DateTime.UtcNow;
        return _mapper.Map<UpdateUserResponse>(carToUpdate);
    }
}