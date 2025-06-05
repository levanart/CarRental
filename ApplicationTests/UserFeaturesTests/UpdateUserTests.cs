using AutoMapper;
using CarRental.Application.Common.Exceptions;
using CarRental.Application.Features.UserFeatures.UpdateUser;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.UserFeaturesTests
{
    [TestFixture]
    public class UpdateUserHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private UpdateUserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            
            _handler = new UpdateUserHandler(_unitOfWorkMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldUpdateUserAndReturnResponse()
        {
            var userId = Guid.NewGuid();
            var originalUser = new User 
            { 
                Id = userId, 
                Username = "OriginalUsername",
                Email = "original@email.com",
                DateUpdated = DateTime.UtcNow.AddDays(-1)
            };

            var request = new UpdateUserRequest(userId, "UpdatedUsername", "updated@email.com", null, null);

            var expectedResponse = new UpdateUserResponse
            {
                Id = userId,
                Username = "UpdatedUsername",
                Email = "updated@email.com"
            };

            _userRepositoryMock.Setup(x => x.GetById(userId, CancellationToken.None))
                .ReturnsAsync(originalUser);
            
            _mapperMock.Setup(x => x.Map(request, originalUser))
                .Callback<UpdateUserRequest, User>((r, u) => 
                {
                    u.Username = r.Username!;
                    u.Email = r.Email!;
                });
                
            _mapperMock.Setup(x => x.Map<UpdateUserResponse>(originalUser))
                .Returns(expectedResponse);
            
            var result = await _handler.Handle(request, CancellationToken.None);
            
            _userRepositoryMock.Verify(x => x.GetById(userId, CancellationToken.None), Times.Once);
            _mapperMock.Verify(x => x.Map(request, originalUser), Times.Once);
            _unitOfWorkMock.Verify(x => x.Save(CancellationToken.None), Times.Once);
            _mapperMock.Verify(x => x.Map<UpdateUserResponse>(originalUser), Times.Once);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.Username, Is.EqualTo("UpdatedUsername"));
            Assert.That(result.Email, Is.EqualTo("updated@email.com"));
            
            Assert.That(originalUser.DateUpdated, Is.EqualTo(DateTime.UtcNow).Within(1).Seconds);
        }

        [Test]
        public void Handle_UserNotFound_ShouldThrowNotFoundException()
        {
            var userId = Guid.NewGuid();
            var request = new UpdateUserRequest(userId, null, null, null, null);

            _userRepositoryMock.Setup(x => x.GetById(userId, CancellationToken.None))
                .ReturnsAsync((User?)null);
            
            var ex = Assert.ThrowsAsync<NotFoundException>(() => 
                _handler.Handle(request, CancellationToken.None));
            
            Assert.That(ex.Message, Is.EqualTo($"User with id {userId} not found"));
            
            _userRepositoryMock.Verify(x => x.GetById(userId, CancellationToken.None), Times.Once);
            _mapperMock.Verify(x => x.Map(It.IsAny<UpdateUserRequest>(), It.IsAny<User>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.Save(CancellationToken.None), Times.Never);
        }

        [Test]
        public async Task Handle_PartialUpdate_ShouldOnlyUpdateProvidedFields()
        {
            var userId = Guid.NewGuid();
            var originalUser = new User 
            { 
                Id = userId, 
                Username = "OriginalUsername",
                Email = "original@email.com",
                PhoneNumber = "1234567890"
            };

            var request = new UpdateUserRequest(userId, null, "updated@email.com", null, null);

            _userRepositoryMock.Setup(x => x.GetById(userId, CancellationToken.None))
                .ReturnsAsync(originalUser);
                
            _mapperMock.Setup(x => x.Map(request, originalUser))
                .Callback<UpdateUserRequest, User>((r, u) => 
                {
                    if (r.Email != null) u.Email = r.Email;
                });
            
            await _handler.Handle(request, CancellationToken.None);
            
            _mapperMock.Verify(x => x.Map(request, originalUser), Times.Once);
            Assert.That(originalUser.Username, Is.EqualTo("OriginalUsername"));
            Assert.That(originalUser.Email, Is.EqualTo("updated@email.com"));
            Assert.That(originalUser.PhoneNumber, Is.EqualTo("1234567890"));
        }
    }
}