using SpotifyAPI.Web;
using SpotifyClone.Models;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;

public class SpotifyService : ISpotifyService
{
    private readonly IConfiguration configuration;
    IManagerSpotifyLocalStorageService SpotifyLocalStorageService;
    private SpotifyClient SpotifyClient;
    private User user = null;

    public SpotifyService(IConfiguration _configuration, IManagerSpotifyLocalStorageService spotifyLocalStorageService)
    {

        configuration = _configuration;
        SpotifyLocalStorageService = spotifyLocalStorageService;
    }

    public async Task<bool> InitilizeUser()
    {
        if (user is not null)
        {
            return true;
        }

        var session = await SpotifyLocalStorageService.GetUserSession();
        if (session is null)
            return false;

        try
        {
            await this.DefineAccessToken(session.Token);
            await this.GetSpotifyUser();
            return this.user is not null;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task GetSpotifyUser()
    {
        var userInfo = await this.SpotifyClient.UserProfile.Current();
        this.user = User.PrivateUserConvertUser(userInfo);
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

    public async Task Logout()
    {
        await SpotifyLocalStorageService.RemoveUserSession();
    }

    public User GetUser()
    {
        return user;
    }
    public async Task<Playlist[]> SearchUserPlaylist(int offset, int limit = 50)
    {
        List<Playlist> ListPlaylist = new List<Playlist>();
        try
        {
            // we need the first page
            var page = await SpotifyClient.Playlists.CurrentUsers();
            page.Offset = offset;
            page.Limit = limit;

            await foreach (var item in SpotifyClient.Paginate(page))
            {
                ListPlaylist.Add(Playlist.FullPlaylistConvertPlaylist(item));
                // you can use "break" here!
            }
            return ListPlaylist.ToArray();
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            await this.Logout();
            return null;
        }
    }

}
