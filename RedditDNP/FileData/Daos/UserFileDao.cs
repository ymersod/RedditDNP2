using ServerLogic.Daos;
using Shared.Models;

namespace FileData.Daos;

public class UserFileDao : IUserDao
{
    private readonly FileContext Context;

    public UserFileDao(FileContext fileContext)
    {
        Context = fileContext;
    }
    public Task<User> RegisterUserAsync(User user)
    {
        Context.Users.Add(user);
        Context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> CheckRegistrationData(string username)
    {
        User? existing = Context.Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetUser(string username)
    {
        User? exsting = Context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exsting);
    }
}