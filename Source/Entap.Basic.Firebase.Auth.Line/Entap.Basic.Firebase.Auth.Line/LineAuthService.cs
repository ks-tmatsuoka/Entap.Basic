using System;
using System.Net;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Line;

namespace Entap.Basic.Firebase.Auth.Line
{
    public class LineAuthService : SnsAuthService, ISnsAuthService
    {
        readonly LineAuthParameter _authParameter;
        IAuthErrorCallback _callback;
        public LineAuthService(LineAuthParameter authParameter, IAuthErrorCallback callback = null)
        {
            _authParameter = authParameter;
            _callback = callback;
        }

        public async Task SignInAsync()
        {
            try
            {
                var parameter = new Entap.Basic.Auth.Line.LineAuthParameter("1655277852", "485bc2555ad821dd085d4ca5998cc242", "openid", "https://entapshiro.page.link/auth_callback");
                var authService = new Entap.Basic.Auth.Line.LineAuthService(parameter);
                var token = await authService.LoginAsync();

                var customToken = new Api.CustomAuthToken(token.AccessToken, token.IdToken);
                var serverToken = await BasicAuthStartUp.AuthApi.PostAuthLineToken(customToken);
                var firebaseCustomToken = await BasicAuthStartUp.AuthApi.PostAuthFirebaseCustomToken();
                await BasicAuthStartUp.AuthApi.PostAuthLineUser(customToken);
                await SignInWithCustomTokenAsync(firebaseCustomToken.CustomToken);
            }
            catch (Exception ex)
            {
                await _callback.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }

        public Task SignOutAsync()
        {
            return Task.CompletedTask;
        }
    }
}
