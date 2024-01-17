using Microsoft.AspNetCore.Components.Authorization;
using SpotifyAPI.Web;
using SpotifyClone.Autenticacao;
using SpotifyClone.Models;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;

public class SpotifyService : ISpotifyService
{
    private readonly IConfiguration configuration;
    CustomAuthenticationStateProvider AuthenticationProvider;
    private SpotifyClient SpotifyClient;
    private User user = null;

    public SpotifyService(IConfiguration _configuration, AuthenticationStateProvider provider)
    {
        configuration = _configuration;
        AuthenticationProvider = provider as CustomAuthenticationStateProvider;
    }

    public async Task<bool> InitilizeUser()
    {
        if (user is not null)
        {
            return true;
        }
        var token = await AuthenticationProvider.GetToken();
        if (string.IsNullOrEmpty(token))
            return false;
        try
        {
            await this.DefineAccessToken(token);
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
        await AuthenticationProvider.UpdateAuthenticationState(null);
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

    public async Task<Artist[]> SearchTopArtists(int limit)
    {
        var artistas = await this.SpotifyClient.Personalization.GetTopArtists();
        var artistsList = artistas?.Items?.Take(limit);
        var artists = artistsList.Select(x => Artist.FullArtistConvertArtist(x)).ToArray();
        return artists;
    }

    public async Task ExecuteSong(string uriDaMusica)
    {
        PlayerAddToQueueRequest request = new PlayerAddToQueueRequest(uriDaMusica);
        var adicionarResultado = await SpotifyClient.Player.AddToQueue(request);
        await this.SpotifyClient.Player.SkipNext();
    }


    public async Task<Music[]> SearchSongs(int offset = 0, int limit = 50)
    {

        var minhasMusicas = await SpotifyClient.Library.GetTracks(new LibraryTracksRequest
        {
            Offset = offset,
            Limit = limit,
        });
        Console.WriteLine("procurando musica" + minhasMusicas.Total);
        return minhasMusicas.Items.Select(x => Music.SavedTrackConvertMusic(x.Track)).ToArray();
    }

    public async Task<Playlist> buscarMusicasPlaylist(string playlistId, int offset = 0, int limit = 50)
    {
        var fullPlaylist = await SpotifyClient.Playlists.Get(playlistId);
        Music[] musicas = null;
        // Verifica se há faixas na playlist
        if (fullPlaylist?.Tracks != null)
        {
            var playlistTracks = await SpotifyClient.Playlists.GetItems(playlistId, new PlaylistGetItemsRequest
            {
                Offset = offset,
                Limit = limit
            });
            // Mapeia as faixas do Spotify para a sua classe Music
            musicas = playlistTracks?.Items?.Select(item => Music.SpotifyTrackConvertMusic(item.Track as FullTrack)).ToArray();
        }

        return new Playlist(fullPlaylist.Id, fullPlaylist.Name, fullPlaylist.Images.FirstOrDefault()?.Url, musicas);
    }

    public Task NextSong()
    {
        throw new NotImplementedException();
    }

    public Task PreviousSong()
    {
        throw new NotImplementedException();
    }

    public async Task<Music> GetCurrentSong()
    {
        var request = new PlayerCurrentlyPlayingRequest(PlayerCurrentlyPlayingRequest.AdditionalTypes.Track);
        var CurrentlyPlaying = await SpotifyClient.Player.GetCurrentlyPlaying(request);
        var song = Music.SpotifyTrackConvertMusic(CurrentlyPlaying.Item as FullTrack);
        return song;
    }
}