using Shared.Models;

namespace ServerLogic.Daos;

public interface IUserDao
{
    Task<User> RegisterUserAsync(User user);
    Task<User?> CheckRegistrationData(string username);
    Task<User?> GetUser(string username);

}