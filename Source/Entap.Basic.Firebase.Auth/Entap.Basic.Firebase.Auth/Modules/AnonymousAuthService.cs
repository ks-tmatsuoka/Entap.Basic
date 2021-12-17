using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class AnonymousAuthService : IAnonymousAuthService
    {
        private readonly IAuthErrorCallback _errorCallback;

        public AnonymousAuthService(IAuthErrorCallback errorCallback)
        {
            _errorCallback = errorCallback;
        }

        public async Task SignInAsync()
        {
            try
            {
                var result = await CrossFirebaseAuth.Current.Instance.SignInAnonymouslyAsync();
                await AuthHelper.StoreServerAccessTokenAsync();
            }
            catch (Exception ex)
            {
                AuthHelper.TrySignOut();
                await _errorCallback?.HandleSignInErrorAsync(ex);
                throw;
            }
        }
    }
}
