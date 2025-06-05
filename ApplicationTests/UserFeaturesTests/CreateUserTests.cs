using AutoMapper;
using CarRental.Application.Features.UserFeatures.CreateUser;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests;

[TestFixture]
public class CreateUserTests
{
    private Mock<IUserRepository> _userRepository;
    private Mock<IUnitOfWork> _unitOfWork;
    private IMapper _mapper;
    private CreateUserHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();


        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserMapper>());
        _mapper = config.CreateMapper();


        _handler = new CreateUserHandler(_unitOfWork.Object, _userRepository.Object, _mapper);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldCreateNewUser()
    {
        var request = new CreateUserRequest("User1", "1234qwerty", "email@fads.com", "798565153");
        
        _unitOfWork.Setup(user => user.Save(CancellationToken.None)).Returns(Task.CompletedTask);
        
        var response = await _handler.Handle(request, CancellationToken.None);
        
        _userRepository.Verify(req => req.Create(It.Is<User>(
            user => user.Email == request.Email &&  user.Username == request.Username), CancellationToken.None), Times.Once);
        
        _unitOfWork.Verify(u => u.Save(CancellationToken.None), Times.Once);
        
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Email, Is.EqualTo(request.Email));
        Assert.That(response.Username, Is.EqualTo(request.Username));
    }

    [Test]
    public void CreateUserValidator_InvalidEmail_ShouldThrowValidationException()
    {
        var validator = new CreateUserValidator();
        var request = new CreateUserRequest("User1", "1234qwerty", "eahjk234.com", "798565153");
        
        var result = validator.Validate(request);
        
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors.Any(e => e.PropertyName == "Email"));
    }
}