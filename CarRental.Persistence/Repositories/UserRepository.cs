using CarRental.Application.Repositories;
using CarRental.Domain.Entity;
using CarRental.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Persistence.Repositories;

public class UserRepository :  BaseRepository<User>, IUserRepository
{
    public UserRepository(CarRentalDbContext context) : base(context)
    {
    }
    
    

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}