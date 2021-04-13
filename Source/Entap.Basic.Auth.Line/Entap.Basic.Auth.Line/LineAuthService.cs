using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Entap.Basic.Auth.Line
{
    public class LineAuthService
    {
        const string AuthBaseUri = "https://access.line.me/oauth2/v2.1/authorize";
        public LineAuthService()
        {
        }

        #region Authorize
        public async Task<LineAuthResponse> AuthorizeAsync(LineAuthRequest request)
        {
            var result = await WebAuthenticator.AuthenticateAsync(
                UriService.GetUri(AuthBaseUri, request),
                new Uri(request.RedirectUri));
            return GetLineAuthResponse(result);
        }

        LineAuthResponse GetLineAuthResponse(WebAuthenticatorResult result)
        {
            var parameters = result?.Properties;
            if (parameters is null)
                throw new ArgumentNullException();

            return UriService.GetQueryObject<LineAuthResponse>(parameters);
        }
        #endregion
    }
}
