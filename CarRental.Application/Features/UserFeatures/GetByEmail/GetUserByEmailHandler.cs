using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetById;
using CarRental.Application.Repositories;
using MediatR;

namespace CarRental.Application.Features.UserFeatures.GetByEmail;

public sealed class GetUserByEmailHandler : IRequestHandler<GetUserByEmailRequest, GetUserByEmailResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByEmailHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserByEmailResponse> Handle(
        GetUserByEmailRequest request, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email,  cancellationToken);
        return _mapper.Map<GetUserByEmailResponse>(user);
    }
}