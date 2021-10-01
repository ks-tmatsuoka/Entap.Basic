using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Apple;
using Entap.Basic.Auth.Apple.Abstract;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth.Apple
{
    public class AppleAuthService : SnsAuthService, ISnsAuthService
    {
        const string ProviderId = "apple.com";
        readonly IAuthErrorCallback _errorCallback;
        AuthorizationScope[] _scopes;
        public AppleAuthService(IAuthErrorCallback errorCallback = null, params AuthorizationScope[] scopes)
        {
            _errorCallback = errorCallback;
            _scopes = scopes;
        }

        public async Task SignInAsync()
        {
            try
            {
                var service = new AppleSignInService(_scopes);
                var id = await service.SignInAsync();
                var credential = CrossFirebaseAuth.Current.OAuthProvider.GetCredential(ProviderId, id.IdToken);

                await CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);
            }
            catch (Exception ex)
            {
                await _errorCallback.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }

        public Task SignOutAsync()
        {
            return Task.CompletedTask;
        }
    }
}
