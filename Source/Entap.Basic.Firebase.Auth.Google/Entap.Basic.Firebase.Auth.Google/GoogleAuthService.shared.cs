using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;
using AuthService = Entap.Basic.Auth.Google.GoogleAuthService;

namespace Entap.Basic.Firebase.Auth.Google
{
    public class GoogleAuthService : SnsAuthService, IGoogleAuthService
    {
        static readonly string ProviderId = CrossFirebaseAuth.Current.GoogleAuthProvider.ProviderId;
        readonly IAuthErrorCallback _errorCallback;
        public GoogleAuthService(IAuthErrorCallback errorCallback = null)
        {
            _errorCallback = errorCallback;
        }

        public async Task SignInAsync()
        {
            try
            {
                var authService = new AuthService();
                var authentication = await authService.AuthAsync();
                var credential = CrossFirebaseAuth.Current.GoogleAuthProvider.GetCredential(authentication.IdToken, authentication.AccessToken);
                await SignInWithCredentialAsync(credential);
                await AuthHelper.StoreServerAccessTokenAsync();
            }
            catch (Exception ex)
            {
                AuthHelper.TrySignOut();
                await _errorCallback?.HandleSignInErrorAsync(ex);
                throw;
            }
        }

        public Task SignOutAsync()
        {
            var authService = new AuthService();
            return authService.SignOutAsync();
        }

        public async Task LinkAsync()
        {
            try
            {
                var provider = new OAuthProvider(ProviderId);
                await LinkWithProviderAsync(provider);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleLinkErrorAsync(ex);
                throw;
            }

        }

        public async Task UnlinkAsync()
        {
            try
            {
                await UnlinkAsync(ProviderId);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleUnlinkErrorAsync(ex);
                throw;
            }
        }
    }
}
