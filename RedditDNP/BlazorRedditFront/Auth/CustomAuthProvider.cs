using System.Security.Claims;
using BlazorRedditFront.Services.Http;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorRedditFront.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IAuthService AuthService;

        public CustomAuthProvider(IAuthService authService)
        {
            AuthService = authService;
            authService.OnAuthStateChanged += AuthStateChanged;
        }
    
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = await AuthService.GetAuthAsync();
        
            return new AuthenticationState(principal);
        }

        private void AuthStateChanged(ClaimsPrincipal principal)
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(principal)));
        }
}