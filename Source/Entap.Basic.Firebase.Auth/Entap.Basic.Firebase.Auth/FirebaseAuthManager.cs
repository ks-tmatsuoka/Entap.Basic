using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Abstractions;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class FirebaseAuthManager : IAuthManager
    {
        readonly IAuthErrorCallback _authErrorCallback;
        IUserDataRepository UserDataRepository => BasicFirebaseAuthStartUp.UserDataRepository;
        public FirebaseAuthManager(IAuthErrorCallback authErrorCallback = null)
        {
            _authErrorCallback = authErrorCallback;
        }

        public bool IsSignedIn => AuthHelper.IsSignedIn;

        public bool IsPasswordAuthSupported => PasswordAuthService is not null;
        public IPasswordAuthService PasswordAuthService => BasicFirebaseAuthStartUp.PasswordAuthService;

        public bool IsTwitterAuthSupported => TwitterAuthService is not null;
        public ITwitterAuthService TwitterAuthService => BasicFirebaseAuthStartUp.TwitterAuthService;

        public bool IsFacebookAuthSupported => FacebookAuthService is not null;
        public IFacebookAuthService FacebookAuthService => BasicFirebaseAuthStartUp.FacebookAuthService;

        public bool IsLineAuthSupported => LineAuthService is not null;
        public ILineAuthService LineAuthService => BasicFirebaseAuthStartUp.LineAuthService;

        public bool IsGoogleAuthSupported => GoogleAuthService is not null;
        public IGoogleAuthService GoogleAuthService => BasicFirebaseAuthStartUp.GoogleAuthService;

        public bool IsAppleAuthSupported => AppleAuthService is not null;
        public IAppleAuthService AppleAuthService => BasicFirebaseAuthStartUp.AppleAuthService;

        public bool IsAnonymousAuthSupported => AnonymousAuthService is not null;
        public IAnonymousAuthService AnonymousAuthService => BasicFirebaseAuthStartUp.AnonymousAuthService;

        public virtual Task SignOutAsync()
        {
            if (!IsSignedIn)
                throw new InvalidOperationException();

            try
            { 
                CrossFirebaseAuth.Current.Instance.SignOut();
                UserDataRepository.RemoveAccessToken();
            }
            catch (Exception ex)
            {
                _authErrorCallback?.HandleSignOutErrorAsync(ex);
                throw;
            }
            return Task.CompletedTask;
        }

        public virtual async Task RefreshServerTokenAsync()
            => await AuthHelper.StoreServerAccessTokenAsync();

        public virtual async Task DeleteUserAsync()
        {
            await BasicFirebaseAuthStartUp.AuthApi.DelerteUser();
            BasicFirebaseAuthStartUp.UserDataRepository.RemoveAccessToken();
        }
    }
}
