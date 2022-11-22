using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ServerLogic.Daos;
using Shared.Models;

namespace EfcDataAccess.Daos;

public class UserEfcDao : IUserDao
{

    private readonly DataBaseContext _dataBaseContext;

    public UserEfcDao(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }
    
    public async Task<User> RegisterUserAsync(User user)
    {
        EntityEntry<User> newUser = await _dataBaseContext.Users.AddAsync(user);
        await _dataBaseContext.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> CheckRegistrationData(string username)
    {
        User? existing = await _dataBaseContext.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(username.ToLower()));
        return existing;
    }

    public async Task<User?> GetUser(string username)
    {
        User? existing = await _dataBaseContext.Users.FirstOrDefaultAsync(user =>
            user.Username.ToLower().Equals(username.ToLower()));
        return existing;
    }
}