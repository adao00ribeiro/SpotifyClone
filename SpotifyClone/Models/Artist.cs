using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace SpotifyClone.Models;

public class Artist
{

    public string Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<Music> Songs { get; set; }

    public Artist()
    {
        Id = "";
        Name = "";
        ImageUrl = "";
    }
    public Artist(string id, string name, string imageUrl)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
    }

    public static Artist FullArtistConvertArtist(FullArtist artist)
    {

        return new Artist(
          artist.Id,
          artist.Name,
          artist.Images?.FirstOrDefault().Url
        );
    }
}
