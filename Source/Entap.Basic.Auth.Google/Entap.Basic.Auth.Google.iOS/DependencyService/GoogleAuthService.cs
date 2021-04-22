using System;
using System.Threading.Tasks;
using Foundation;
using Google.SignIn;
using UIKit;

namespace Entap.Basic.Auth.Google.iOS
{
    public class GoogleAuthService
    {
        public GoogleAuthService()
        {
        }

        public Task<GoogleUser> SignInAsync()
        {
            if (!Platform.Initialized)
                throw new InvalidOperationException("Please call Entap.Basic.Auth.Google.iOS.Platform.Init() method.");
            SignIn.SharedInstance.ClientId = Platform.ClientId;

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
    }
}
