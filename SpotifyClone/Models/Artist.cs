using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyClone.Models;

public class Artist
{

    public string Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<Music> Songs { get; set; }
    public Artist(string id, string name, string imageUrl, List<Music> songs)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        Songs = songs;
    }

}
