using System;
using System.Threading.Tasks;
using Entap.Basic.Api;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    internal static class AuthHelper
    {
        public static bool IsSignedIn => CurrentUser is not null;
        public static IUser CurrentUser => CrossFirebaseAuth.Current?.Instance?.CurrentUser;

        public static async Task StoreServerAccessTokenAsync()
        {
            var _userDataRepository = BasicFirebaseAuthStartUp.UserDataRepository;
            if (!IsSignedIn)
                throw new InvalidOperationException();

            var idToken = await CurrentUser.GetIdTokenAsync(true);
            var serverToken = await BasicFirebaseAuthStartUp.AuthApi.PostAuthFirebaseToken(new FirebaseIdToken(idToken));
            await _userDataRepository.SetAccessTokenAsync(serverToken.AccessToken);
        }

        public static void TrySignOut()
        {
            if (!IsSignedIn)
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
