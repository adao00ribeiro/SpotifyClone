using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyClone.Models;

namespace SpotifyClone.Services.Interfaces;

public interface IPlayerService
{
    public Action<Music> musicaAtual { get; set; }
    void definirMusicaAtual(Music musica);
    Task obterMusicaAtual();
    Task voltarMusica();
    Task proximaMusica();
}
