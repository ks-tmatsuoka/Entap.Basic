using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;
using AuthService = Entap.Basic.Auth.Google.GoogleAuthService;

namespace Entap.Basic.Firebase.Auth.Google
{
    public class GoogleAuthService : SnsAuthService, IGoogleAuthService
    {
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
                throw ex;
            }
        }

        public Task SignOutAsync()
        {
            var authService = new AuthService();
            return authService.SignOutAsync();
        }
    }
}
