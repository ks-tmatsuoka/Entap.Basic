using System;
using System.Net;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Line;

namespace Entap.Basic.Firebase.Auth.Line
{
    public class LineAuthService : SnsAuthService, ILineAuthService
    {
        IAuthErrorCallback _callback;

        public LineAuthService(IAuthErrorCallback callback = null)
        {
            _callback = callback;
        }

        public async Task SignInAsync()
        {
            try
            {
                var authService = new Entap.Basic.Auth.Line.LineAuthService();
                var token = await authService.LoginAsync();
                var customToken = new Api.CustomAuthToken(token.AccessToken, token.IdToken);
                var serverToken = await BasicFirebaseAuthStartUp.AuthApi.PostAuthLineToken(customToken);
                await BasicFirebaseAuthStartUp.UserDataRepository.SetAccessTokenAsync(serverToken);
                var firebaseCustomToken = await BasicFirebaseAuthStartUp.AuthApi.PostAuthFirebaseCustomToken();
                await BasicFirebaseAuthStartUp.AuthApi.PostAuthLineUser(customToken);
                await SignInWithCustomTokenAsync(firebaseCustomToken.CustomToken);
                await AuthHelper.StoreServerAccessTokenAsync();
            }
            catch (Exception ex)
            {
                AuthHelper.TrySignOut();
                BasicFirebaseAuthStartUp.UserDataRepository.RemoveAccessToken();
                await _callback.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }
    }
}
