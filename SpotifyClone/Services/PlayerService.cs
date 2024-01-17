using SpotifyClone.Models;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;
public class PlayerService : IPlayerService
{
    private Action<Music> _musicaAtual;
    public Action<Music> musicaAtual
    {
        get { return _musicaAtual; }
        set { _musicaAtual = value; }
    }
    private ISpotifyService SpotifyService;

    public PlayerService(ISpotifyService spotifyService)
    {
        SpotifyService = spotifyService;
    }
    public async Task obterMusicaAtual()
    {
        var musica = await SpotifyService.GetCurrentSong();
        this.definirMusicaAtual(musica);
    }
    public void definirMusicaAtual(Music musica)
    {
        this._musicaAtual?.Invoke(musica);
    }

    public async Task voltarMusica()
    {
        await this.SpotifyService.PreviousSong();
    }

    public async Task proximaMusica()
    {
        await this.SpotifyService.NextSong();
    }

}