using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using AuthenticationServices;
using Entap.Basic.Auth.Apple.Abstract;
using Entap.Basic.Auth.Apple.iOS;
using Foundation;
using UIKit;

namespace Entap.Basic.Auth.Apple
{
    public class AppleSignInService : NSObject, IAppleSignInService, IASAuthorizationControllerDelegate, IASAuthorizationControllerPresentationContextProviding
    {
        static ASAuthorizationScope[] _scopes;
        static bool _isInitialized;

#nullable enable
        public static void Init(params AuthorizationScope[]? scopes)
#nullable disable
        {
            PlatformInit(scopes?.ToASAuthorizationScopes());
        }

#nullable enable
        public static void PlatformInit(params ASAuthorizationScope[]? scopes)
#nullable disable
        {
            _scopes = scopes;
            _isInitialized = true;
        }

        public AppleSignInService()
        {
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

            if (!_isInitialized)
                throw new InvalidOperationException($"Please call {nameof(AppleSignInService.Init)} method.");

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

            if ((_scopes?.Any((scope) => scope == ASAuthorizationScope.Email) == true) &&
                creds.Email is null)
            {
                var jwt = NSString.FromData(creds.IdentityToken, NSStringEncoding.UTF8);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                if (token.Payload.TryGetValue("email", out var email))
                    creds.SetValueForKey(new NSString(email.ToString()), new NSString(nameof(creds.Email)));
            }
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

        public static async Task<ASAuthorizationAppleIdProviderCredentialState> GetCredentialStateAsync(string userId)
        {
            var appleIdProvider = new ASAuthorizationAppleIdProvider();
            var credentialState = await appleIdProvider.GetCredentialStateAsync(userId);
            return credentialState;
        }

        /// <summary>
        /// AppleID使用停止時の処理を登録
        /// </summary>
#nullable enable
        public static async Task RegisterCredentialRevokedActionAsync(string? userId, Action action)
#nullable disable
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var status = await GetCredentialStateAsync(userId);
                if (status == ASAuthorizationAppleIdProviderCredentialState.Revoked)
                    action.Invoke();
            }
            AddCredentialRevokedObserver(action);
        }

        /// <summary>
        /// アプリ起動中のAppleID使用停止時の処理を登録
        /// </summary>
        static void AddCredentialRevokedObserver(Action action)
        {
            var center = NSNotificationCenter.DefaultCenter;
            center.AddObserver(ASAuthorizationAppleIdProvider.CredentialRevokedNotification, (_) =>
            {
                action.Invoke();
            });
        }
    }
}
