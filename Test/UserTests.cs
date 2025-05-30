using CarRental.Domain.Entity;

namespace Test;

[TestFixture]
public class UserTests
{
    [Test]
    public void CreateUser()
    {
        var email = "test@gmail.com";
        var password = "password";
        var username = "test";

        
        var user = new User
        {
            Email = email,
            Password = password,
            Username = username
        };
        
        
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.Username, Is.EqualTo(username));
        Assert.That(user.Password, Is.EqualTo(password));
    }

    [Test]
    public void UpdateUser()
    {
        var email = "test@gmail.com";
        var password = "password";
        var username = "test";
        var newUsername = "newName";

        
        var user = new User
        {
            Email = email,
            Password = password,
            Username = username
        };
        user.Username = newUsername;
        
        
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.Username, Is.EqualTo(newUsername));
        Assert.That(user.Password, Is.EqualTo(password));
    }
}