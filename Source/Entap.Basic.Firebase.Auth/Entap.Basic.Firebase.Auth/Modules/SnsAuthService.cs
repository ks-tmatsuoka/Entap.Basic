using System;
using System.Threading.Tasks;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class SnsAuthService
    {
        public SnsAuthService()
        {
        }

        protected Task<IAuthResult> SignInWithProviderAsync(IFederatedAuthProvider provider)
            => CrossFirebaseAuth.Current.Instance.SignInWithProviderAsync(provider);

        protected Task<IAuthResult> SignInWithCredentialAsync(IAuthCredential credential)
            => CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);

        protected Task<IAuthResult> SignInWithCustomTokenAsync(string token)
            => CrossFirebaseAuth.Current.Instance.SignInWithCustomTokenAsync(token);

        protected Task<IAuthResult> LinkWithCredentialAsync(IAuthCredential credential)
            => CrossFirebaseAuth.Current.Instance.CurrentUser.LinkWithCredentialAsync(credential);

        protected Task<IAuthResult> LinkWithProviderAsync(IFederatedAuthProvider provider)
            => CrossFirebaseAuth.Current.Instance.CurrentUser.LinkWithProviderAsync(provider);

        protected Task<IUser> UnlinkAsync(string providerId)
            => CrossFirebaseAuth.Current.Instance.CurrentUser.UnlinkAsync(providerId);
    }
}
