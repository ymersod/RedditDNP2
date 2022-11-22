using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class  User
{
    public string Username { get; init; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string domain { get; set; }
    public string Nickname { get; set; }
    public string Role { get; set; }
    public int Age { get; set; }
    public int SecurityLevel { get; set; }
    public User(){}
}