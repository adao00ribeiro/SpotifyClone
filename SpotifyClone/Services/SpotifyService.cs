using SpotifyAPI.Web;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;

public class SpotifyService : ISpotifyService
{

    public readonly string authEndPoint;
    public readonly string clientId;
    public readonly string redirectUrl;
    public readonly string[] scopes;
    public SpotifyClient SpotifyClient;
    public SpotifyService(string authEndPoint, string clientId, string redirectUrl, string[] scopes)
    {

        this.authEndPoint = authEndPoint;
        this.clientId = clientId;
        this.redirectUrl = redirectUrl;
        this.scopes = scopes;
    }

    public void inicializarUsuario()
    {

    }

    public void obterSpotifyUsuario()
    {
       
    }

    public string GetUrlLogin()
    {
        var tempauthEndpoint = $"{authEndPoint}?";
        var tempclientId = $"client_id={clientId}&";
        var tempredirectUrl = $"redirect_uri={redirectUrl}&";
        string tempScopes = $"scope={string.Join("%20", scopes)}&";
        var tempresponseType = "response_type=token&show_dialog=true";
        return tempauthEndpoint + tempclientId + tempredirectUrl + tempScopes + tempresponseType;
       
    }

    public string GetTokenUrlCallback(string url)
    {
        // Criar um objeto Uri com a URL
        Uri uri = new Uri(url);
        var maxLen = Math.Min(1, uri.Fragment.Length);
        Dictionary<string, string> fragmentParams = uri.Fragment.Substring(maxLen)?
          .Split("&", StringSplitOptions.RemoveEmptyEntries)?
          .Select(param => param.Split("=", StringSplitOptions.RemoveEmptyEntries))?
          .ToDictionary(param => param[0], param => param[1]) ?? new Dictionary<string, string>();

        var _isAuthed = fragmentParams.ContainsKey("access_token");
        if (_isAuthed)
        {
            return fragmentParams["access_token"];
        }
        return "";
    }

    public async Task DefineAccessToken(string token)
    {
        if (token == null)
        {
            return;
        }
        SpotifyClient = new SpotifyClient(token);
    }


}
