using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;using BlazorRedditFront;
using BlazorRedditFront.Auth;
using BlazorRedditFront.Services.Http;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Shared.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped(sp => new HttpClient());

builder.Services.AddMudServices();
AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();