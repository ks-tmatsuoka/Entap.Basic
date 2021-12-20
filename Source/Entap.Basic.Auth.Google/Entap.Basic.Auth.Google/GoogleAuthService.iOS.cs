using System;
using System.Threading.Tasks;
using Foundation;
using Google.SignIn;
using UIKit;
using Platform = Entap.Basic.Auth.Google.iOS.Platform;

namespace Entap.Basic.Auth.Google
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public GoogleAuthService()
        {
            if (!Platform.Initialized)
                throw new InvalidOperationException("Please call Entap.Basic.Auth.Google.iOS.Platform.Init() method.");

            SignIn.SharedInstance.ClientId = Platform.ClientId;
        }

        public Task<GoogleUser> SignInAsync()
        {
            var viewController = Platform.GetViewController.Invoke();
            if (viewController is null)
                throw new NullReferenceException();
            SignIn.SharedInstance.PresentingViewController = viewController;

            var completionSource = new TaskCompletionSource<GoogleUser>();

            EventHandler<SignInDelegateEventArgs> signedIn = null;
            signedIn += (sender, e) =>
            {
                SignIn.SharedInstance.SignedIn -= signedIn;
                if (e.Error is null)
                    completionSource.TrySetResult(e.User);
                else if (e.Error.Code == (int)ErrorCode.Canceled)
                    completionSource.SetCanceled();
                else
                    completionSource.SetException(new NSErrorException(e.Error));
            };
            SignIn.SharedInstance.SignedIn += signedIn;

            
            if (SignIn.SharedInstance.HasPreviousSignIn)
                SignIn.SharedInstance.RestorePreviousSignIn();
            else
                SignIn.SharedInstance.SignInUser();

            return completionSource.Task;
        }

        public void SignOut()
        {
            SignIn.SharedInstance.SignOutUser();
        }

        #region IGoogleAuthService
        public async Task<Authentication> AuthAsync()
        {
            var user = await SignInAsync();
            return new Authentication
            {
                AccessToken = user.Authentication.AccessToken,
                AccessTokenExpirationDate = (DateTime)user.Authentication.AccessTokenExpirationDate,
                IdToken = user.Authentication.IdToken,
                IdTokenExpirationDate = (DateTime)user.Authentication.IdTokenExpirationDate,
                RefreshToken = user.Authentication.RefreshToken
            };
        }

        public Task SignOutAsync()
        {
            SignOut();
            return Task.CompletedTask;
        }
        #endregion

        public static bool OnOpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return SignIn.SharedInstance.HandleUrl(url);
        }
    }
}
