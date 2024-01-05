using Blazored.LocalStorage;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;

public class ManagerSpotifyLocalStorageService : IManagerSpotifyLocalStorageService
{
    const string key = "tokenspotify";

    private readonly ILocalStorageService localStorageService;

    public ManagerSpotifyLocalStorageService(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public async Task<string> GetToken()
    {
        return await this.localStorageService.GetItemAsync<string>(key);

    }
    public async Task RemoveToken()
    {
        await this.localStorageService.RemoveItemAsync(key);
    }

    public async Task SaveToken(string token)
    {
        await this.localStorageService.SetItemAsync(key, token);
    }

}
