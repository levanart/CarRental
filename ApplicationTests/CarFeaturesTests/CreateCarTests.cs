using AutoMapper;
using CarRental.Application.Features.CarFeatures.CreateCar;
using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using Moq;

namespace ApplicationTests.CarFeaturesTests
{
    [TestFixture]
    public class CreateCarTests
    {
        private Mock<ICarRepository> _carRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private CreateCarHandler _handler;

        [SetUp]
        public void Setup()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            
            _handler = new CreateCarHandler(
                _unitOfWorkMock.Object,
                _carRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldCreateCarAndReturnResponse()
        {
            var request = new CreateCarRequest(
                PlateNumber: "ABC123",
                Brand: "Toyota",
                Model: "Camry",
                Power: 180,
                Mileage: 15000,
                ReleaseYear: 2022);

            var carEntity = new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = request.PlateNumber,
                Brand = request.Brand,
                Model = request.Model,
                Power = request.Power,
                Mileage = request.Mileage,
                ReleaseYear = request.ReleaseYear
            };

            var expectedResponse = new CreateCarResponse
            {
                Id = carEntity.Id,
                PlateNumber = carEntity.PlateNumber,
                Brand = carEntity.Brand,
                Model = carEntity.Model,
                Power = carEntity.Power,
                Mileage = carEntity.Mileage,
                ReleaseYear = carEntity.ReleaseYear
            };

            _mapperMock.Setup(m => m.Map<Car>(request))
                .Returns(carEntity);
                
            _mapperMock.Setup(m => m.Map<CreateCarResponse>(carEntity))
                .Returns(expectedResponse);

            _carRepositoryMock.Setup(r => r.Create(carEntity, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
                
            _unitOfWorkMock.Setup(u => u.Save(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            var result = await _handler.Handle(request, CancellationToken.None);
            
            _mapperMock.Verify(m => m.Map<Car>(request), Times.Once);
            _carRepositoryMock.Verify(r => r.Create(carEntity, It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Save(It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(m => m.Map<CreateCarResponse>(carEntity), Times.Once);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(carEntity.Id));
            Assert.That(result.PlateNumber, Is.EqualTo(request.PlateNumber));
            Assert.That(result.Brand, Is.EqualTo(request.Brand));
            Assert.That(result.Model, Is.EqualTo(request.Model));
            Assert.That(result.Power, Is.EqualTo(request.Power));
            Assert.That(result.Mileage, Is.EqualTo(request.Mileage));
            Assert.That(result.ReleaseYear, Is.EqualTo(request.ReleaseYear));
        }

        [Test]
        public void Handle_InvalidRequest_ShouldThrowValidationException()
        {
            var invalidRequest = new CreateCarRequest(
                PlateNumber: "",
                Brand: "",
                Model: "",
                Power: -100,
                Mileage: -5000,
                ReleaseYear: 1884
            );
            
            var validator = new CreateCarValidator();
            var validationResult = validator.Validate(invalidRequest);
            
            _mapperMock.Setup(m => m.Map<Car>(It.IsAny<CreateCarRequest>()))
                .Throws(new InvalidOperationException("Mapper should not be called for invalid requests"));

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => 
                _handler.Handle(invalidRequest, CancellationToken.None));

            Assert.That(ex, Is.Not.Null);
        }
    }
}