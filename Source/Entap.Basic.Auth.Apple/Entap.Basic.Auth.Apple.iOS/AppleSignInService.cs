using System;
using System.Threading.Tasks;
using AuthenticationServices;
using Entap.Basic.Auth.Apple.Abstract;
using Entap.Basic.Auth.Apple.iOS;
using Foundation;
using UIKit;

namespace Entap.Basic.Auth.Apple
{
    public class AppleSignInService : NSObject, IAppleSignInService, IASAuthorizationControllerDelegate, IASAuthorizationControllerPresentationContextProviding
    {
        readonly ASAuthorizationScope[] _scopes;
        public AppleSignInService(params ASAuthorizationScope[] scopes)
        {
            _scopes = scopes;
        }

        public AppleSignInService(params AuthorizationScope[] scopes)
        {
            _scopes = scopes.ToASAuthorizationScopes();
        }

        TaskCompletionSource<ASAuthorizationAppleIdCredential> tcsCredential;

        #region IAppleSignInService
        public async Task<AppleIdCredential> SignInAsync()
        {
            var credential = await GetCredential();
            return credential.ToAppleIdCredential();
        }
        #endregion

        public async Task<ASAuthorizationAppleIdCredential> GetCredential()
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                throw new NotSupportedException();

            var appleIdProvider = new ASAuthorizationAppleIdProvider();
            var request = appleIdProvider.CreateRequest();
            request.RequestedScopes = _scopes;

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
            if (error.Code == (int)ASAuthorizationError.Canceled)
                tcsCredential?.SetException(new OperationCanceledException());
            else
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
