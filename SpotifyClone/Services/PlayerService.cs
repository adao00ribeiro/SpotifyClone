using SpotifyClone.Models;
using SpotifyClone.Services.Interfaces;

namespace SpotifyClone.Services;
public class PlayerService : IPlayerService
{
    public Action<Music> musicaAtual { get; set; }
    private Timer timer;
    private ISpotifyService SpotifyService;

    public PlayerService(ISpotifyService spotifyService)
    {
        SpotifyService = spotifyService;
        Task tas = this.obterMusicaAtual();
        tas.Wait();
    }

    public async Task obterMusicaAtual()
    {
        ClearTimer();
        // Obtenho a musica
        var musica = await this.SpotifyService.GetCurrentSong();
        this.definirMusicaAtual(musica);
        timer = new Timer(async _ => await obterMusicaAtual(), null, 0, 5000);
    }

    public void definirMusicaAtual(Music musica)
    {
        this.musicaAtual?.Invoke(musica);
    }

    public async Task voltarMusica()
    {
        await this.SpotifyService.PreviousSong();
    }

    public async Task proximaMusica()
    {
        await this.SpotifyService.NextSong();
    }
    private void ClearTimer()
    {
        timer.Change(Timeout.Infinite, Timeout.Infinite);
    }
}