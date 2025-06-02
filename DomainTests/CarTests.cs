using CarRental.Domain.Entity;

namespace Test;

[TestFixture]
public class CarTests
{
    [Test]
    public void CreateCar()
    {
        var id = Guid.NewGuid();
        var dateCreated = DateTime.UtcNow;
        var dateUpdated = DateTime.UtcNow;
        var dateDeleted = DateTime.UtcNow;
        var isActive = true;
        var isDeleted = false;
        var plateNumber = "123456789";
        var brand = "BMW";
        var model = "MK";
        var power = 123;
        var mileage = 123;
        var releaseYear = 2020;

        
        var car = new Car
        {
            Id = id,
            DateCreated = dateCreated,
            DateUpdated = dateUpdated,
            DateDeleted = dateDeleted,
            IsActive = isActive,
            IsDeleted = isDeleted,
            PlateNumber = plateNumber,
            Brand = brand,
            Model = model,
            Power = power,
            Mileage = mileage,
            ReleaseYear = releaseYear,
        };
        
        
        Assert.That(car.Id, Is.EqualTo(id));
        Assert.That(car.DateCreated, Is.EqualTo(dateCreated));
        Assert.That(car.DateUpdated, Is.EqualTo(dateUpdated));
        Assert.That(car.DateDeleted, Is.EqualTo(dateDeleted));
        Assert.That(car.IsActive, Is.EqualTo(isActive));
        Assert.That(car.IsDeleted, Is.EqualTo(isDeleted));
        Assert.That(car.PlateNumber, Is.EqualTo(plateNumber));
        Assert.That(car.Brand, Is.EqualTo(brand));
        Assert.That(car.Model, Is.EqualTo(model));
        Assert.That(car.Power, Is.EqualTo(power));
        Assert.That(car.Mileage, Is.EqualTo(mileage));
        Assert.That(car.ReleaseYear, Is.EqualTo(releaseYear));
    }

    [Test]
    public void UpdateCar()
    {
        var id = Guid.NewGuid();
        var dateCreated = DateTime.UtcNow;
        var dateUpdated = DateTime.UtcNow;
        DateTime? dateDeleted = null;
        var isActive = true;
        var isDeleted = false;
        var plateNumber = "123456789";
        var brand = "BMW";
        var model = "MK";
        var power = 123;
        var mileage = 123;
        var releaseYear = 2020;
        var newMileage = 123;
        var newReleaseYear = 2023;
        var newDateUpdated = DateTime.UtcNow;

        
        var car = new Car
        {
            Id = id,
            DateCreated = dateCreated,
            DateUpdated = dateUpdated,
            DateDeleted = dateDeleted,
            IsActive = isActive,
            IsDeleted = isDeleted,
            PlateNumber = plateNumber,
            Brand = brand,
            Model = model,
            Power = power,
            Mileage = mileage,
            ReleaseYear = releaseYear,
        };
        car.Mileage = newMileage;
        car.ReleaseYear = newReleaseYear;
        car.DateUpdated = newDateUpdated;
        
        
        Assert.That(car.Id, Is.EqualTo(id));
        Assert.That(car.DateCreated, Is.EqualTo(dateCreated));
        Assert.That(car.DateUpdated, Is.EqualTo(newDateUpdated));
        Assert.That(car.DateDeleted, Is.EqualTo(dateDeleted));
        Assert.That(car.IsActive, Is.EqualTo(isActive));
        Assert.That(car.IsDeleted, Is.EqualTo(isDeleted));
        Assert.That(car.PlateNumber, Is.EqualTo(plateNumber));
        Assert.That(car.Brand, Is.EqualTo(brand));
        Assert.That(car.Model, Is.EqualTo(model));
        Assert.That(car.Power, Is.EqualTo(power));
        Assert.That(car.Mileage, Is.EqualTo(newMileage));
        Assert.That(car.ReleaseYear, Is.EqualTo(newReleaseYear));
    }
}