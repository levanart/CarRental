using AutoMapper;
using CarRental.Application.Features.CarFeatures.GetById;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetById;

public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserByIdResponse> Handle(
        GetUserByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id,  cancellationToken);
        return _mapper.Map<GetUserByIdResponse>(user);
    }
}