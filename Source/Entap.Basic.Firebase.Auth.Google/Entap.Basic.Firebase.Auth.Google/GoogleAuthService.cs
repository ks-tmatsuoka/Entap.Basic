using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Plugin.FirebaseAuth;
using AuthService = Entap.Basic.Auth.Google.GoogleAuthService;

namespace Entap.Basic.Firebase.Auth.Google
{
    public class GoogleAuthService : SnsAuthService, ISnsAuthService
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
            }
            catch (Exception ex)
            {
                await _errorCallback.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }
    }
}
