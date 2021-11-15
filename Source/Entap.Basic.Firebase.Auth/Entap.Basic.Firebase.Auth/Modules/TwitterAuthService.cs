using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class TwitterAuthService : SnsAuthService, ISnsAuthService
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
            }
            catch(Exception ex)
            {
                await _errorCallback?.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }

        public Task SignOutAsync()
        {
            return Task.CompletedTask;
        }
    }
}
