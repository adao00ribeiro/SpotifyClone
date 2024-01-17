using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyClone.Models;

namespace SpotifyClone.Services.Interfaces;

public interface ISpotifyService
{
    string GetUrlLogin();
    string GetTokenUrlCallback(string url);
    Task DefineAccessToken(string token);
    Task<bool> InitilizeUser();
    User GetUser();
    Task<Artist[]> SearchTopArtists(int limit);
    Task Logout();
    Task<Music[]> SearchSongs(int offset = 0, int limit = 50);
    Task<Playlist[]> SearchUserPlaylist(int offset, int limit = 50);
    Task<Playlist> buscarMusicasPlaylist(string playlistId, int offset = 0, int limit = 50);
    Task ExecuteSong(string Id);
    Task SetCurrentSong(Music music);
    Task GetSpotifyUser();
    Task NextSong();
    Task PreviousSong();
    Task<Music> GetCurrentSong();
}

