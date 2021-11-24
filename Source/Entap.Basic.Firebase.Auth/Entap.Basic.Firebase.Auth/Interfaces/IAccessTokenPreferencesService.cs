using System;
using System.Threading.Tasks;
using Entap.Basic.Api;

namespace Entap.Basic.Firebase.Auth
{
    public interface IAccessTokenPreferencesService
    {
        Task<string> GetAccessTokenAsync();
        Task SetAccessTokenAsync(ServerAccessToken accessToken);
        void RemoveAccessToken();
    }
}
