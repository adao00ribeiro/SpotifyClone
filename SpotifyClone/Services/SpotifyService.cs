using Microsoft.AspNetCore.Components.Authorization;
using SpotifyAPI.Web;
using SpotifyClone.Autenticacao;
using SpotifyClone.Models;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;

public class SpotifyService : ISpotifyService
{
    private readonly IConfiguration configuration;
    public SpotifyClient SpotifyClient;
    private User user ;

    public SpotifyService(IConfiguration _configuration){
        
        configuration = _configuration;
    }

    public void inicializarUsuario()
    {

    }

    public void obterSpotifyUsuario()
    {
       
    }

    public string GetUrlLogin()
    {
        var tempauthEndpoint = $"{configuration["SpotifyConfiguration:authEndPoint"]}?";
        var tempclientId = $"client_id={configuration["SpotifyConfiguration:clientId"]}&";
        var tempredirectUrl = $"redirect_uri={configuration["SpotifyConfiguration:redirectUrl"]}&";
        string tempScopes = $"scope={string.Join("%20", configuration.GetSection("SpotifyConfiguration:scopes").Get<string[]>())}&";
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

    public void Logout()
    {
        throw new NotImplementedException();
    }

    public User GetUser()
    {
        return user;
    }
}
