using System.ComponentModel.DataAnnotations;
using ServerLogic.Daos;
using Shared.Dtos;
using Shared.Models;

namespace ServerLogic.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> RegisterAsync(UserCreationDto userToCreate)
    {
        User? existing = await userDao.CheckRegistrationData(userToCreate.Username);
        if (existing != null)
        {
            throw new Exception("Username already taken!");
        }

        ValidateData(userToCreate);

        User user = new User
        {
            Username = userToCreate.Username,
            Password = userToCreate.Password,
            Age = userToCreate.Age,
            Email = userToCreate.Email,
            Nickname = userToCreate.Nickname,
            domain = "reddit",
            Role = "commoner",
            SecurityLevel = 0
        };

        User createdUser = await userDao.RegisterUserAsync(user);

        return createdUser;
    }

    public async Task<User> ValidateUserAsync(string username, string password)
    {
        User? existingUser = await userDao.GetUser(username);
            
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    private static void ValidateData(UserCreationDto userCreationDto)
    {
        if (string.IsNullOrEmpty(userCreationDto.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(userCreationDto.Password))
        {
            throw new ValidationException("Password cannot be null");
        }

        if (string.IsNullOrEmpty(userCreationDto.Email))
        {
            throw new ValidationException("Email cannot be null");
        }
        
        if (string.IsNullOrEmpty(userCreationDto.Nickname))
        {
            throw new ValidationException("Nickname cannot be null");
        }
    }
}