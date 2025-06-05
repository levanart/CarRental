using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetByUsername;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests;

[TestFixture]
public class GetUserByUsernameTests
{
    private Mock<IUserRepository> _userRepository;
    private IMapper _mapper;
    private GetUserByUsernameHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();


        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserByUsernameMapper>());
        _mapper = config.CreateMapper();


        _handler = new GetUserByUsernameHandler(_userRepository.Object, _mapper);
    }

    [Test]
    public async Task GetUserByUsername_ValidRequest_ShouldReturnUser()
    {
        var username = "username";
        _userRepository.Setup(repo => repo.GetByUsernameAsync(username, CancellationToken.None)).ReturnsAsync(new User {Username = username});
        
        var result = await _handler.Handle(new GetUserByUsernameRequest(username), CancellationToken.None);
        
        Assert.That(result.Username, Is.EqualTo(username));
    }

    [Test]
    public async Task GetUserByUsername_InvalidRequest_ShouldReturnNull()
    {
        var username = "username";
        _userRepository.Setup(repo => repo.GetByUsernameAsync(username, CancellationToken.None)).ReturnsAsync((User?)null);
        
        var result = await _handler.Handle(new GetUserByUsernameRequest(username), CancellationToken.None);
        
        Assert.That(result, Is.Null);
    }
}