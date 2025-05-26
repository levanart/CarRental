using AutoMapper;
using CarRental.Application.Common.Exceptions;
using CarRental.Application.Features.CarFeatures.DeleteCar;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.UserFeatures.DeleteUser;

public class DeleteUserHandler :  IRequestHandler<DeleteUserRequest, DeleteUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public DeleteUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);
        if (user == null) throw new NotFoundException($"User  with Id: {request.Id} not found!");
        await _userRepository.DeleteUser(user, cancellationToken);
        await _unitOfWork.Save(cancellationToken);
        return _mapper.Map<DeleteUserResponse>(user);
    }
}