using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace SpotifyClone.Models;

public class Playlist
{
    public string Id {get;set;}
    public string Name{get;set;}
    public string imageUrl{get;set;}
    public Music[] Musics{get;set;}
    public Playlist(string id, string name, string imageUrl, Music[] musics)
    {
        Id = id;
        Name = name;
        this.imageUrl = imageUrl;
        Musics = musics;
    }
    
}
