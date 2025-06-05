using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetById;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests;

[TestFixture]
public class GetUserByIdTests
{
    private Mock<IUserRepository> _userRepository;
    private IMapper _mapper;
    private GetUserByIdHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();


        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetUserByIdMapper>());
        _mapper = config.CreateMapper();


        _handler = new GetUserByIdHandler(_userRepository.Object, _mapper);
    }

    [Test]
    public async Task GetUserById_ValidRequest_ShouldReturnUser()
    {
        var id = Guid.NewGuid();
        _userRepository.Setup(repo => repo.GetById(id, CancellationToken.None)).ReturnsAsync(new User {Id = id});
        
        var result = await _handler.Handle(new GetUserByIdRequest(id), CancellationToken.None);
        
        Assert.That(result.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task GetUserById_InvalidRequest_ShouldReturnNull()
    {
        var id = Guid.NewGuid();
        _userRepository.Setup(repo => repo.GetById(id, CancellationToken.None)).ReturnsAsync((User?)null);
        
        var result = await _handler.Handle(new GetUserByIdRequest(id), CancellationToken.None);
        
        Assert.That(result, Is.Null);
    }
}