using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeRedditor", a =>
                a.RequireAuthenticatedUser().RequireClaim("Domain", "reddit"));
            
            options.AddPolicy("SecurityLevel3", a =>
                a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "2", "3"));

            options.AddPolicy("SecurityLevel4", a =>
                a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4"));

            options.AddPolicy("MustBeModerator", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Moderator"));

            options.AddPolicy("MustBeOver18", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    Claim? ageClaim = context.User.FindFirst(claim => claim.Type.Equals("Age"));
                    if (ageClaim == null) return false;
                    return int.Parse(ageClaim.Value) >= 18;
                }));
        });
    }
}