using System;
using System.Threading.Tasks;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class SnsAuthService : AuthService
    {
        readonly IAuthErrorCallback _errorCallback;
        public SnsAuthService(IAuthErrorCallback errorCallback) : base(errorCallback)
        {
            _errorCallback = errorCallback;
        }

        protected Task<IAuthResult> SignInWithProviderAsync(IFederatedAuthProvider provider)
            => CrossFirebaseAuth.Current.Instance.SignInWithProviderAsync(provider);

        protected Task<IAuthResult> SignInWithCredentialAsync(IAuthCredential credential)
            => CrossFirebaseAuth.Current.Instance.SignInWithCredentialAsync(credential);

        protected Task<IAuthResult> SignInWithCustomTokenAsync(string token)
            => CrossFirebaseAuth.Current.Instance.SignInWithCustomTokenAsync(token);
    }
}
