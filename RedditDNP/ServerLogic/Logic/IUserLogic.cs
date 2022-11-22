using Shared.Dtos;
using Shared.Models;

namespace ServerLogic.Logic;

public interface IUserLogic
{
    Task<User> RegisterAsync(UserCreationDto userToCreate);
    Task<User> ValidateUserAsync(string user, string password);
}