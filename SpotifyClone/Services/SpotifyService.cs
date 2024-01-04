using SpotifyAPI.Web;
using SpotifyClone.Models;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;

public class SpotifyService : ISpotifyService
{
    private readonly IConfiguration _configuration;
    private SpotifyClient spotifyApi;
    private User user;

    public SpotifyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void inicializarUsuario()
    {

    }

    public void obterSpotifyUsuario()
    {

    }

    public string obterUrlLogin()
    {
        /*
        const authEndpoint = `${SpotifyConfiguration.authEndpoint}?`;
        const clientId = `client_id=${SpotifyConfiguration.clientId}&`;
        const redirectUrl = `redirect_uri=${SpotifyConfiguration.redirectUrl}&`;
        const scopes = `scope=${SpotifyConfiguration.scopes.join('%20')}&`;
        const responseType = `response_type=token&show_dialog=true`;
        return authEndpoint + clientId + redirectUrl + scopes + responseType; 
      */
        return _configuration["SpotifyConfiguration:authEndPoint"];
    }

    public void obterTokenUrlCallback()
    {

    }

    public void definirAccessToken(string token)
    {

    }


}
