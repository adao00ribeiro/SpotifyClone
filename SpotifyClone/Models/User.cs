using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace SpotifyClone.Models;

public class User
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string imageUrl { get; private set; }
    
    public User(string id, string name, string imageUrl)
    {
        Id = id;
        Name = name;
        this.imageUrl = imageUrl;
    }

    internal static User PrivateUserConvertUser(PrivateUser userInfo)
    {
        return new User(
            userInfo.Id,
            userInfo.DisplayName,
            userInfo.Images.FirstOrDefault().Url
        );
    }
}
