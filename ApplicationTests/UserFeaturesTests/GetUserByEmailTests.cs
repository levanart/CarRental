using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetByEmail;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests;

[TestFixture]
public class GetUserByEmailTests
{
    private Mock<IUserRepository> _userRepository;
    private IMapper _mapper;
    private GetUserByEmailHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();


        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserByEmailMapper>());
        _mapper = config.CreateMapper();


        _handler = new GetUserByEmailHandler(_userRepository.Object, _mapper);
    }

    [Test]
    public async Task GetUserByEmail_ValidRequest_ShouldReturnUser()
    {
        var email = "test@example.com";
        _userRepository.Setup(repo => repo.GetByEmailAsync(email, CancellationToken.None)).ReturnsAsync(new User {Email = email});
        
        var result = await _handler.Handle(new GetUserByEmailRequest(email), CancellationToken.None);
        
        Assert.That(result.Email, Is.EqualTo(email));
    }

    [Test]
    public async Task GetUserByEmail_InvalidRequest_ShouldReturnNull()
    {
        var email = "test@example.com";
        _userRepository.Setup(repo => repo.GetByEmailAsync(email, CancellationToken.None)).ReturnsAsync((User?)null);
        
        var result = await _handler.Handle(new GetUserByEmailRequest(email), CancellationToken.None);
        
        Assert.That(result, Is.Null);
    }
}