using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SpotifyClone;
using SpotifyClone.Autenticacao;
using SpotifyClone.Services;
using SpotifyClone.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
Console.WriteLine(builder.Configuration["SpotifyConfiguration:authEndPoint"]);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
