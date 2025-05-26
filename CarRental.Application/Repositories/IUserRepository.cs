using CarRental.Application.Features.CarFeatures.DeleteCar;
using CarRental.Application.Features.UserFeatures.UpdateUser;
using CarRental.Domain.Entity;

namespace CarRental.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByPhoneAsync(string phone, CancellationToken cancellationToken);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<User> DeleteUser(User userToDelete, CancellationToken cancellationToken);
}