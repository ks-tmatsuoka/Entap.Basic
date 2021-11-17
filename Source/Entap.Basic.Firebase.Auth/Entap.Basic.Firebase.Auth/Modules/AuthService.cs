using System;
using System.Threading.Tasks;
using Entap.Basic.Api;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class AuthService
    {
        IAuthErrorCallback _callback;
        public AuthService(IAuthErrorCallback callback)
        {
            _callback = callback;
        }

        IUser CurrentUser => CrossFirebaseAuth.Current?.Instance?.CurrentUser;
        public async Task StoreServerAccessTokenAsync()
        {
            try
            {
                var _userDataRepository = BasicFirebaseAuthStartUp.UserDataRepository;
                if (CurrentUser is null)
                    throw new InvalidOperationException("Not SignIn");

                var idToken = await Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance.CurrentUser.GetIdTokenAsync(true);
                var serverToken = await BasicFirebaseAuthStartUp.AuthApi.PostAuthFirebaseToken(new FirebaseIdToken(idToken));
                await _userDataRepository.SetAccessTokenAsync(serverToken.AccessToken);
            }
            catch (Exception ex)
            {
                SignOut();
                await _callback?.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }

        void SignOut()
        {
            if (CurrentUser is null)
                return;

            try
            {
                Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance.SignOut();
            }
            catch
            {
            }
        }
    }
}
