using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetByPhone;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetByUsername;

public sealed class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameRequest, GetUserByUsernameResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByUsernameHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserByUsernameResponse> Handle(
        GetUserByUsernameRequest request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username,  cancellationToken);
        return _mapper.Map<GetUserByUsernameResponse>(user);
    }
}