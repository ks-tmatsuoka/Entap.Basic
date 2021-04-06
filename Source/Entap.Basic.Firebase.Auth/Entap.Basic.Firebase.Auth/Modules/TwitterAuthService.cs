using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class TwitterAuthService : SnsAuthService, ISnsAuthService
    {
        const string ProviderId = "twitter.com";

        public TwitterAuthService()
        {
        }

        public Task SignInAsync()
        {
            var provider = new OAuthProvider(ProviderId);
            return SignInWithProviderAsync(provider);
        }
    }
}
