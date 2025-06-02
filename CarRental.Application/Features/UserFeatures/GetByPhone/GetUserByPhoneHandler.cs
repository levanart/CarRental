using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetByEmail;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetByPhone;

public sealed class GetUserByPhoneHandler : IRequestHandler<GetUserByPhoneRequest, GetUserByPhoneResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByPhoneHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserByPhoneResponse> Handle(
        GetUserByPhoneRequest request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByPhoneAsync(request.PhoneNumber,  cancellationToken);
        return _mapper.Map<GetUserByPhoneResponse>(user);
    }
}