using System.Security.Claims;
using Shared.Models;

namespace BlazorRedditFront.Services.Http;

public interface IAuthService
{
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task RegisterAsync(string userName, string password, int age, string email, string nickName);
    public Task<ClaimsPrincipal> GetAuthAsync();
    
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}