using AutoMapper;
using CarRental.Application.Common.Exceptions;
using CarRental.Application.Features.UserFeatures.DeleteUser;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests;

[TestFixture]
public class DeleteUserTests
{
    private Mock<IUserRepository> _userRepository;
    private Mock<IUnitOfWork> _unitOfWork;
    private IMapper _mapper;
    private DeleteUserHandler _handler;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IUserRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();


        var config = new MapperConfiguration(cfg => cfg.AddProfile<DeleteUserMapper>());
        _mapper = config.CreateMapper();


        _handler = new DeleteUserHandler(_unitOfWork.Object, _userRepository.Object, _mapper);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldDeleteUser()
    {
        var id = Guid.NewGuid();
        var request = new DeleteUserRequest(id);
        _unitOfWork.Setup(unitOfWork => unitOfWork.Save(CancellationToken.None)).Returns(Task.CompletedTask);
        var testUser = new User { Id = id };
        _userRepository.Setup(repo => repo.GetById(id, CancellationToken.None)).ReturnsAsync(testUser);

        var response = await _handler.Handle(request, CancellationToken.None);

        _userRepository.Verify(repo => repo.GetById(id, CancellationToken.None), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.Save(CancellationToken.None), Times.Once);
        _userRepository.Verify(repo => repo.Delete(testUser, CancellationToken.None), Times.Once);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.Id, Is.EqualTo(id));
    }

    [Test]
    public void Handle_UserNotFound_ShouldThrowException()
    {
        var id = Guid.NewGuid();
        var request = new DeleteUserRequest(id);
        _userRepository.Setup(repo => repo.GetById(id, CancellationToken.None)).ReturnsAsync((User?)null);
        
        var exception = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        Assert.That(exception, Is.Not.Null);
    }
}