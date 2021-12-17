using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class TwitterAuthService : SnsAuthService, ITwitterAuthService
    {
        static readonly string ProviderId = CrossFirebaseAuth.Current.TwitterAuthProvider.ProviderId;
        readonly IAuthErrorCallback _errorCallback;
        public TwitterAuthService(IAuthErrorCallback errorCallback = null)
        {
            _errorCallback = errorCallback;
        }

        public async Task SignInAsync()
        {
            try
            {
                var provider = new OAuthProvider(ProviderId);
                await SignInWithProviderAsync(provider);
                await AuthHelper.StoreServerAccessTokenAsync();
            }
            catch(Exception ex)
            {
                AuthHelper.TrySignOut();
                await _errorCallback?.HandleSignInErrorAsync(ex);
                throw;
            }
        }
    }
}
