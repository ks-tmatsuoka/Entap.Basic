using System;
using System.Threading.Tasks;

namespace Entap.Basic.Firebase.Auth
{
    public class UserDataRepository : IUserDataRepository
    {
        IAccessTokenPreferencesService _preferencesService;
        public UserDataRepository(IAccessTokenPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;
        }

        public virtual Task<string> GetAccessToken()
            => _preferencesService.GetAccessTokenAsync();

        public virtual async Task SetAccessTokenAsync(string accessToken)
            => await _preferencesService.SetAccessTokenAsync(accessToken);

        public virtual void RemoveAccessToken()
            => _preferencesService.RemoveAccessToken();
    }
}
