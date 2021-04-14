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
                var authService = new Entap.Basic.Auth.Line.LineAuthService();
                _authParameter.AuthRequest.UpdateState();
                var result = await authService.AuthorizeAsync(_authParameter.AuthRequest);
                if (result.State != _authParameter.AuthRequest.State)
                    throw new InvalidOperationException();
                _authParameter.AccessTokenRequest.Code = result.Code;
                var (status, token) = await authService.GetAccessTokenAsync(_authParameter.AccessTokenRequest);
                if (status != HttpStatusCode.OK)
                    throw new HttpListenerException((int)status);

                var serverToken = await BasicAuthStartUp.AuthApi.PostAuthLineToken(new Api.CustomAuthToken(token.AccessToken, token.IdToken));
                await SignInWithCustomTokenAsync(serverToken.AccessToken);
            }
            catch (Exception ex)
            {
                await _callback.HandleSignInErrorAsync(ex);
                throw ex;
            }
        }
    }
}
