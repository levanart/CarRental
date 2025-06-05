using AutoMapper;
using CarRental.Application.Features.UserFeatures.GetAllUsers;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests;

[TestFixture]
public class GetAllUserTests
{
    private Mock<IUserRepository> _userRepository;
    private IMapper _mapper;
    private GetAllUserHandler _handler;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IUserRepository>();

        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetAllUserMapper>());
        _mapper = config.CreateMapper();

        _handler = new GetAllUserHandler(_mapper, _userRepository.Object);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldReturnAllUsers()
    {
        var users = new List<User>
        {
            new User { Username = "User1" },
            new User { Username = "User2" }
        };

        var request = new GetAllUserRequest();
        _userRepository.Setup(repo => repo.GetAllAsync(CancellationToken.None)).ReturnsAsync(users);


        var response = await _handler.Handle(request, CancellationToken.None);


        _userRepository.Verify(repo => repo.GetAllAsync(CancellationToken.None), Times.Once);
        Assert.That(response, Is.Not.Null);
        Assert.That(response[0].Username, Is.EqualTo("User1"));
        Assert.That(response[1].Username, Is.EqualTo("User2"));
    }

    [Test]
    public async Task Handle_EmptyRepository_ShouldReturnEmptyList()
    {
        var request = new GetAllUserRequest();
        _userRepository.Setup(repo => repo.GetAllAsync(CancellationToken.None)).ReturnsAsync(new List<User>());


        var response = await _handler.Handle(request, CancellationToken.None);


        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Empty);
    }
}