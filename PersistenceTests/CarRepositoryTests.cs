using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using CarRental.Persistence.Context;
using CarRental.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestFixture]
public class CarRepositoryTests
{
    private CarRentalDbContext _context;
    private ICarRepository _carRepository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CarRentalDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CarRentalDbContext(options);
        _carRepository = new CarRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
    
    private readonly List<Car> _testCars = new()
    {
        new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "A123BC",
                Brand = "Toyota",
                Model = "Camry",
                Power = 180,
                Mileage = 125000,
                ReleaseYear = 2015,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "B456DE",
                Brand = "Toyota",
                Model = "Camry",
                Power = 200,
                Mileage = 80000,
                ReleaseYear = 2018,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "C789FG",
                Brand = "Honda",
                Model = "Civic",
                Power = 150,
                Mileage = 90000,
                ReleaseYear = 2016,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "D012HI",
                Brand = "Honda",
                Model = "Civic",
                Power = 170,
                Mileage = 60000,
                ReleaseYear = 2019,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "E345JK",
                Brand = "BMW",
                Model = "X5",
                Power = 300,
                Mileage = 50000,
                ReleaseYear = 2017,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "F678LM",
                Brand = "BMW",
                Model = "X5",
                Power = 350,
                Mileage = 30000,
                ReleaseYear = 2020,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "G901NO",
                Brand = "Mercedes-Benz",
                Model = "E-Class",
                Power = 250,
                Mileage = 70000,
                ReleaseYear = 2016,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "H234PQ",
                Brand = "Mercedes-Benz",
                Model = "E-Class",
                Power = 280,
                Mileage = 40000,
                ReleaseYear = 2019,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "J567RS",
                Brand = "Audi",
                Model = "A4",
                Power = 190,
                Mileage = 85000,
                ReleaseYear = 2015,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            },
            new Car
            {
                Id = Guid.NewGuid(),
                PlateNumber = "K890TU",
                Brand = "Audi",
                Model = "A4",
                Power = 220,
                Mileage = 55000,
                ReleaseYear = 2018,
                DateCreated = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            }
    };

    private async Task GenerateCarsList()
    {
        _testCars.ForEach((c) =>
        {
            _carRepository.Create(c, CancellationToken.None);
        });
        
        await _context.SaveChangesAsync();
    }

    [Test]
    public async Task CreateCarsAndGetAll()
    {
        await GenerateCarsList();
        
        var result = _carRepository.GetAllAsync(CancellationToken.None).Result;
        
        Assert.That(result, Is.EqualTo(_testCars));
    }

    [Test]
    public async Task GetByBrandAdnModel()
    {
        await GenerateCarsList();
        
        var brand = "BMW";
        var model = "X5";
        
        var result = await _carRepository.GetByBrandAdnModel(brand, model, CancellationToken.None);
        
        Assert.That(result.Count, Is.EqualTo(2));
        result.ForEach((c) =>
        {
            Assert.That(c.Brand, Is.EqualTo(brand));
            Assert.That(c.Model, Is.EqualTo(model));
        });
    }

    [Test]
    public async Task GetByPlate()
    {
        await GenerateCarsList();
        
        var plate = "B456DE";
        
        var result = await _carRepository.GetByPlate(plate, CancellationToken.None);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result.PlateNumber, Is.EqualTo(plate));
    }

    [Test]
    public async Task GetById()
    {
        var id = Guid.NewGuid();
        
        Car car = new()
        {
            Id = id,
            DateCreated = new DateTime(2022, 05, 15, 10, 30, 00),
            DateUpdated = new DateTime(2023, 01, 20, 14, 15, 00),
            DateDeleted = null,
            IsActive = true,
            IsDeleted = false,
            PlateNumber = "А777БВ77",
            Brand = "Lada",
            Model = "Vesta",
            Power = 122,
            Mileage = 42500,
            ReleaseYear = 2019
        };
        
        await _carRepository.Create(car, CancellationToken.None);
        await _context.SaveChangesAsync();
        
        await _carRepository.GetById(id,  CancellationToken.None);
        
        Assert.That(car.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task GetGreaterReleaseYear()
    {
        await GenerateCarsList();
        
        var result = await _carRepository.GetGreaterReleaseYear(2017, CancellationToken.None);

        result.ForEach((c) =>
        {
            Assert.That(c.ReleaseYear, Is.GreaterThanOrEqualTo(2017));
        });
    }
}