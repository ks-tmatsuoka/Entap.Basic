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
                var credential = await GetCredentialAsync();

                await SignInWithCredentialAsync(credential);
                await AuthHelper.StoreServerAccessTokenAsync();
            }
            catch (Exception ex)
            {
                AuthHelper.TrySignOut();
                await _errorCallback?.HandleSignInErrorAsync(ex);
                throw;
            }
        }

        private async Task<IAuthCredential> GetCredentialAsync()
        {
            var service = new AppleSignInService();
            var id = await service.SignInAsync();
            return CrossFirebaseAuth.Current.OAuthProvider.GetCredential(ProviderId, id.IdToken);
        }

        public async Task LinkAsync()
        {
            try
            {
                var credential = await GetCredentialAsync();
                await LinkWithCredentialAsync(credential);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleLinkErrorAsync(ex);
                throw;
            }
        }

        public async Task UnlinkAsync()
        {
            try
            {
                await UnlinkAsync(ProviderId);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleUnlinkErrorAsync(ex);
                throw;
            }
        }
    }
}
