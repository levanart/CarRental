using AutoMapper;
using CarRental.Application.Common.Exceptions;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.UserFeatures.UpdateUser;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UpdateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var userToUpdate = await _userRepository.GetById(request.UserId,  cancellationToken);
        if (userToUpdate == null) throw new NotFoundException($"User with id {request.UserId} not found");
        _mapper.Map(request, userToUpdate);
        userToUpdate.DateUpdated = DateTime.UtcNow;
        await _unitOfWork.Save(cancellationToken);
        return _mapper.Map<UpdateUserResponse>(userToUpdate);
    }
}