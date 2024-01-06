using Blazored.LocalStorage;
using SpotifyClone.Autenticacao;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;

public class ManagerSpotifyLocalStorageService : IManagerSpotifyLocalStorageService
{
    const string key = "UserSession";

    private readonly ILocalStorageService localStorageService;

    public ManagerSpotifyLocalStorageService(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public async Task<UserSession> GetUserSession()
    {
        return await this.localStorageService.GetItemAsync<UserSession>(key);

    }
    public async Task RemoveUserSession()
    {
        await this.localStorageService.RemoveItemAsync(key);
    }

    public async Task SaveUserSession(UserSession session)
    {
        await this.localStorageService.SetItemAsync(key, session);
    }

}
