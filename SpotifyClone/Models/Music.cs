
using SpotifyAPI.Web;
namespace SpotifyClone.Models;

public class Music
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<Artist> Artists { get; set; }
    public Album Album { get; set; }
    public string Time { get; set; }

    public string Uri { get; set; }

    public Music()
    {
        Id = "";
        Title = "";
        Artists = new List<Artist>();
        Album = new Album();
        Time = "0";
        Uri = "";
    }
    public Music(string id, string title, string time, string uri)
    {
        Id = id;
        Title = title;
        Time = time;
        Uri = uri;
    }
    public Music(string id, string title, List<Artist> artists, Album album, string time, string uri)
    {
        Id = id;
        Title = title;
        Artists = artists;
        Album = album;
        Time = time;
        Uri = uri;
    }

    internal static Music SavedTrackConvertMusic(FullTrack spotifyTrack)
    {
        if (spotifyTrack is null)
        {
            return new Music();
        }

        return new Music(
             spotifyTrack.Id,
             spotifyTrack.Name,
             MsParaMinutos(spotifyTrack.DurationMs),
             spotifyTrack.Href
    );
    }


    public static string MsParaMinutos(long ms)
    {
        var data = DateTime.MinValue.AddMilliseconds(ms);
        return data.ToString("mm:ss");
    }

    internal static Music SpotifyTrackConvertMusic(FullTrack? fullTrack)
    {
        if (fullTrack is null)
        {
            return new Music();
        }
        return new Music(
             fullTrack.Id,
             fullTrack.Name,
             MsParaMinutos(fullTrack.DurationMs),
             fullTrack.Album.Images.FirstOrDefault().Url
    );
    }
}



public class Album
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string ImagemUrl { get; set; }
}