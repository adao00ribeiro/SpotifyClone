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
builder.Services.AddSingleton<ISpotifyService, SpotifyService>(provider =>
     {
         // You can use IConfiguration to retrieve values from appsettings.json or use hard-coded values
         var authEndPoint = builder.Configuration["SpotifyConfiguration:authEndPoint"];
         var clientId = builder.Configuration["SpotifyConfiguration:clientId"];
         var redirectUrl = builder.Configuration["SpotifyConfiguration:redirectUrl"];
         var scopes = builder.Configuration.GetSection("SpotifyConfiguration:scopes").Get<string[]>();
         var responseType = builder.Configuration["SpotifyConfiguration:ResponseType"];
         // Create an instance of SpotifyService with the provided values
         return new SpotifyService(authEndPoint, clientId, redirectUrl, scopes);
     });

builder.Services.AddScoped<IManagerSpotifyLocalStorageService, ManagerSpotifyLocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
