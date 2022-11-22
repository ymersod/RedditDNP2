using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Shared.Dtos;
using Shared.Models;

namespace BlazorRedditFront.Services.Http;

public class JwtAuthService : IAuthService
{
    private readonly HttpClient client = new();

    public static string? jwt { get; private set; } = "";
    
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    
    public async Task LoginAsync(string username, string password)
    {
        UserLoginDto userLoginDto = new()
        {
            Username = username,
            Password = password
        };

        string userAsJson = JsonSerializer.Serialize(userLoginDto);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");

        
        HttpResponseMessage response = await client.PostAsync("https://localhost:7206/auth/login", content);
        
        //Should be the Jwt token
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        jwt = token;

        ClaimsPrincipal principal = CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);
    }

    public Task LogoutAsync()
    {
        jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;    
    }

    public async Task RegisterAsync(string userName, string password, int age, string email, string nickName)
    {
        
        string userAsJson = JsonSerializer.Serialize(CreateUser(userName, password, age, email, nickName));
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://localhost:7206/auth/register", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }
    
    
    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
    
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(jwt);
    
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    private UserCreationDto CreateUser(string userName, string password, int age, string email, string nickName)
    {
        UserCreationDto userDTO = new UserCreationDto
        {
            Username = userName,
            Password = password,
            Age = age,
            Email = email,
            Nickname = nickName,
        };
        return userDTO;
    }
    
    
}