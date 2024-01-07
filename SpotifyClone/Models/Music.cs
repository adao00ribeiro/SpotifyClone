using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyClone.Models;

public class Music
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<Artist> Artists { get; set; }
    public Album Album { get; set; }
    public string Time { get; set; }

    public Music(string id, string title, List<Artist> artists, Album album, string time)
    {
        Id = id;
        Title = title;
        Artists = artists;
        Album = album;
        Time = time;
    }
}



public class Album
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string ImagemUrl { get; set; }
}