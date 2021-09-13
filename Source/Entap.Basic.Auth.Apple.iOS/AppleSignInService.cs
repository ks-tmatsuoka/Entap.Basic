using System;
using System.Threading.Tasks;
using AuthenticationServices;
using Foundation;
using UIKit;

namespace Entap.Basic.Auth.Apple.iOS
{
    public class AppleSignInService : NSObject, IASAuthorizationControllerDelegate, IASAuthorizationControllerPresentationContextProviding
    {
        TaskCompletionSource<ASAuthorizationAppleIdCredential> tcsCredential;

        public async Task<ASAuthorizationAppleIdCredential> GetCredential()
        {
            var appleIdProvider = new ASAuthorizationAppleIdProvider();
            var request = appleIdProvider.CreateRequest();
            request.RequestedScopes = new[] { ASAuthorizationScope.Email, ASAuthorizationScope.FullName };

            var authorizationController = new ASAuthorizationController(new[] { request })
            {
                Delegate = this,
                PresentationContextProvider = this
            };
            authorizationController.PerformRequests();

            tcsCredential = new TaskCompletionSource<ASAuthorizationAppleIdCredential>();
            var creds = await tcsCredential.Task;

            return creds;
        }

        #region IASAuthorizationController Delegate

        [Export("authorizationController:didCompleteWithAuthorization:")]
        public void DidComplete(ASAuthorizationController controller, ASAuthorization authorization)
        {
            var creds = authorization.GetCredential<ASAuthorizationAppleIdCredential>();
            tcsCredential?.TrySetResult(creds);
        }

        [Export("authorizationController:didCompleteWithError:")]
        public void DidComplete(ASAuthorizationController controller, NSError error)
        {
            tcsCredential?.SetException(new NSErrorException(error));
        }

        #endregion

        #region IASAuthorizationControllerPresentation Context Providing
        public UIWindow GetPresentationAnchor(ASAuthorizationController controller)
        {
            return UIApplication.SharedApplication.KeyWindow;
        }
        #endregion
    }
}
