using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using CarRental.Persistence.Context;
using CarRental.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests;

[TestFixture]
public class Tests
{
    private CarRentalDbContext _context;
    private IUserRepository _userRepository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CarRentalDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CarRentalDbContext(options);
        _userRepository = new UserRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task CreateUsers()
    {
        var testUsers = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Username = "alex_developer",
                Email = "alex@devcompany.com",
                Password = "Dev@2023Pass",
                PhoneNumber = "+79161234567",
                DateCreated = DateTime.Now,
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "marta_driver",
                Email = "marta.driver@mail.ru",
                Password = "Marta2023!",
                PhoneNumber = "+79037654321",
                DateCreated = DateTime.Now,
                IsActive = true
            },
            new User
            {
                Id = Guid.NewGuid(),
                Username = "admin_super",
                Email = "admin@carrental.org",
                Password = "SuperAdmin123$",
                PhoneNumber = "+74951234567",
                DateCreated = DateTime.Now,
                IsActive = true
            }
        };

        testUsers.ForEach(u => { _userRepository.Create(u, CancellationToken.None); });

        await _context.SaveChangesAsync();

        var result = await _userRepository.GetAllAsync(CancellationToken.None);
        Assert.That(result, Is.EqualTo(testUsers));
    }

    [Test]
    public async Task GetUserByEmail()
    {
        var email = "user1@example.com";
        var password = "SecurePass123!";
        var username = "user1";
        var phoneNumber = "+12345678901";

        var user = new User
        {
            Email = email,
            Password = password,
            Username = username,
            PhoneNumber = phoneNumber
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var result = await _userRepository.GetByEmailAsync(email, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result?.Email, Is.EqualTo(email));
    }

    [Test]
    public async Task GetUserById()
    {
        var id = Guid.NewGuid();
        var email = "user2@example.com";
        var password = "AnotherPass456!";
        var username = "user2";
        var phoneNumber = "+98765432101";

        var user = new User
        {
            Id = id,
            Email = email,
            Password = password,
            Username = username,
            PhoneNumber = phoneNumber
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var result = await _userRepository.GetById(id, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result?.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task GetUserByPhone()
    {
        var email = "user3@example.com";
        var password = "YetAnotherPass789!";
        var username = "user3";
        var phoneNumber = "+11223344556";

        var user = new User
        {
            Email = email,
            Password = password,
            Username = username,
            PhoneNumber = phoneNumber
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var result = await _userRepository.GetByPhoneAsync(phoneNumber, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result?.PhoneNumber, Is.EqualTo(phoneNumber));
    }

    [Test]
    public async Task GetUserByUsername()
    {
        var email = "user4@example.com";
        var password = "StrongPassword123!";
        var username = "user4";
        var phoneNumber = "+99887766554";

        var user = new User
        {
            Email = email,
            Password = password,
            Username = username,
            PhoneNumber = phoneNumber
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var result = await _userRepository.GetByUsernameAsync(username, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result?.Username, Is.EqualTo(username));
    }


// [Test]
    // public async Task DeleteUser()
    // {
    //     var email = "user6@example.com";
    //     var password = "DeleteMePass123!";
    //     var username = "user6";
    //     var phoneNumber = "+66778899001";
    //     
    //     var user = new User
    //     {
    //         Email = email,
    //         Password = password,
    //         Username = username,
    //         PhoneNumber = phoneNumber
    //     };
    //     
    //     await _context.Users.AddAsync(user);
    //     await _context.SaveChangesAsync();
    //     
    //     var result = await _userRepository.GetByUsernameAsync(username, CancellationToken.None);
    //     await _userRepository.DeleteUser(result, CancellationToken.None);
    //     var deletedUser = await _userRepository.GetByEmailAsync(email, CancellationToken.None);
    //     
    //     Assert.That(deletedUser, Is.EqualTo(new User()));
    // }
}