using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class TwitterAuthService : SnsAuthService, ISnsAuthService
    {
        const string ProviderId = "twitter.com";
        readonly Action<Exception> _errorCallback;
        public TwitterAuthService(Action<Exception> errorCallback = null)
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
                _errorCallback.Invoke(ex);
                throw ex;
            }
        }
    }
}
