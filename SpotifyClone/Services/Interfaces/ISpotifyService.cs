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
    void Logout();
    User GetUser();
}
