using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyClone.Services.Interfaces;

public interface IManagerSpotifyLocalStorageService
{
    Task<string> GetToken();
    Task RemoveToken();
    Task SaveToken(string token);
}
