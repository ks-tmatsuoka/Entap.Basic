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

        protected Task SignInWithProviderAsync(IFederatedAuthProvider provider)
            => CrossFirebaseAuth.Current.Instance.SignInWithProviderAsync(provider);

        protected Task SignInWithCredentialAsync(IAuthCredential credential)
            => CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);
    }
}
