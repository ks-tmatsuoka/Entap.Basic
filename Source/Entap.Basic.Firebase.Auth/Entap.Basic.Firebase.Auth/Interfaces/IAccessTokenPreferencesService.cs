using System;
using System.Threading.Tasks;

namespace Entap.Basic.Firebase.Auth
{
    public interface IAccessTokenPreferencesService
    {
        Task<string> GetAccessTokenAsync();
        Task SetAccessTokenAsync(string accessToken);
        void RemoveAccessToken();
    }
}
