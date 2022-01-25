using System;
using System.Net;
using System.Linq;
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

        static LoginScope[] _loginScopes;
        public static void SetLoginScopes(params LoginScope[] scopes)
        {
            _loginScopes = scopes;
        }

        public async Task SignInAsync()
        {
            if (_loginScopes?.Any() != true)
                throw new InvalidOperationException($"Please call {nameof(LineAuthService.SetLoginScopes)} method.");

            try
            {
                var authService = new Entap.Basic.Auth.Line.LineAuthService();
                var result = await authService.LoginAsync(_loginScopes);
                await SignInAsync(result);
            }
            catch (Exception ex)
            {
                AuthHelper.TrySignOut();
                BasicFirebaseAuthStartUp.UserDataRepository.RemoveAccessToken();
                await _callback.HandleSignInErrorAsync(ex);
                throw;
            }
        }

        public async Task SignInAsync(LoginResult result)
        {
            try
            {
                if (result.IsCanceled)
                    throw new TaskCanceledException();

                if (result.IsFaulted)
                    throw result.Exception;

                var customToken = new Api.CustomAuthToken(result.LineAccessToken.AccessToken, result.LineAccessToken.IdToken);
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
                throw;
            }
        }
    }
}
