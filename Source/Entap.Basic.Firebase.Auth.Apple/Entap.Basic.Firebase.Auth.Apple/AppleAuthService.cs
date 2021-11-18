using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Apple;
using Entap.Basic.Auth.Apple.Abstract;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth.Apple
{
    public class AppleAuthService : SnsAuthService, IAppleAuthService
    {
        const string ProviderId = "apple.com";
        readonly IAuthErrorCallback _errorCallback;
        public AppleAuthService(IAuthErrorCallback errorCallback = null)
        {
            _errorCallback = errorCallback;
        }

        public async Task SignInAsync()
        {
            try
            {
                var service = new AppleSignInService();
                var id = await service.SignInAsync();
                var credential = CrossFirebaseAuth.Current.OAuthProvider.GetCredential(ProviderId, id.IdToken);

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
    }
}
