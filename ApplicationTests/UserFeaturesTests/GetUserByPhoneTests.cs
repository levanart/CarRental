using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetByPhone;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests;

[TestFixture]
public class GetUserByPhoneTests
{
    private Mock<IUserRepository> _userRepository;
    private IMapper _mapper;
    private GetUserByPhoneHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();

        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserByPhoneMapper>());
        _mapper = config.CreateMapper();


        _handler = new GetUserByPhoneHandler(_userRepository.Object, _mapper);
    }

    [Test]
    public async Task GetUserByPhone_ValidRequest_ShouldReturnUser()
    {
        var phone = "+79204688953";
        _userRepository.Setup(repo => repo.GetByPhoneAsync(phone, CancellationToken.None)).ReturnsAsync(new User {PhoneNumber = phone});
        
        var result = await _handler.Handle(new GetUserByPhoneRequest(phone), CancellationToken.None);
        
        Assert.That(result.PhoneNumber, Is.EqualTo(phone));
    }

    [Test]
    public async Task GetUserByPhone_InvalidRequest_ShouldReturnNull()
    {
        var phone = "+79204688953";
        _userRepository.Setup(repo => repo.GetByPhoneAsync(phone, CancellationToken.None)).ReturnsAsync((User?)null);
        
        var result = await _handler.Handle(new GetUserByPhoneRequest(phone), CancellationToken.None);
        
        Assert.That(result, Is.Null);
    }
}