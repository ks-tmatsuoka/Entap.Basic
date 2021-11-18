using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Firebase.Auth;
using Plugin.FacebookClient;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth.Facebook
{
    public class FacebookAuthService : SnsAuthService, IFacebookAuthService
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
                await SignInWithCredentialAsync(credential);
                await AuthHelper.StoreServerAccessTokenAsync();
            }
            catch (Exception ex)
            {
                AuthHelper.TrySignOut();
                await _errorCallback.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }

        public async Task FacebookLoginAsync()
        {
            CrossFacebookClient.Current.Logout();
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

        public Task SignOutAsync()
        {
            try
            {
                CrossFacebookClient.Current.Logout();
            }
            catch(Exception ex)
            {
                _errorCallback.HandleSignOutErrorAsync(ex);
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
