using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Firebase.Auth;
using Plugin.FacebookClient;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth.Facebook
{
    public class FacebookAuthService : SnsAuthService, ISnsAuthService
    {
        readonly IAuthErrorCallback _errorCallback;
        public FacebookAuthService(IAuthErrorCallback errorCallback = null)
        {
            _errorCallback = errorCallback;
        }

        public async Task SignInAsync()
        {
            try
            {
                if (CrossFacebookClient.Current.IsLoggedIn)
                    CrossFacebookClient.Current.Logout();

                await FacebookLoginAsync();
                var accessToken = CrossFacebookClient.Current.ActiveToken;

                var credential = CrossFirebaseAuth.Current.FacebookAuthProvider.GetCredential(accessToken);
                await CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);
            }
            catch (Exception ex)
            {
                await _errorCallback.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }

        public async Task FacebookLoginAsync()
        {
            var result = await CrossFacebookClient.Current.LoginAsync(new string[] { "email" });
            switch (result.Status)
            {
                case FacebookActionStatus.Canceled:
                    throw new OperationCanceledException();
                case FacebookActionStatus.Unauthorized:
                    throw new UnauthorizedAccessException();
                case FacebookActionStatus.Error:
                    throw new Exception("Facebook SignIn Error");
            }
        }
    }
}
