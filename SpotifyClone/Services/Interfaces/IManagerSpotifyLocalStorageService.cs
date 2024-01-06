using SpotifyClone.Autenticacao;

namespace SpotifyClone.Services.Interfaces;

public interface IManagerSpotifyLocalStorageService
{
    Task<UserSession> GetUserSession();
    Task RemoveUserSession();
    Task SaveUserSession(UserSession session);
}
