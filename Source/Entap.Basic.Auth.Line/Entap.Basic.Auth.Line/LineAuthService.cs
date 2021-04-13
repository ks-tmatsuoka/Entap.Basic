using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Entap.Basic.Auth.Line
{
    public class LineAuthService
    {
        readonly string AuthBaseUri = "https://access.line.me/oauth2/v2.1/authorize";
        readonly string TokenBaseUri = "https://api.line.me/oauth2/v2.1/token";
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

#nullable enable
        /// <summary>
        /// アクセストークン取得処理
        /// https://developers.line.biz/ja/reference/line-login/#issue-access-token
        /// </summary>
        /// <param name="request">LineAccessTokenRequest</param>
        /// <returns>HttpStatusCode, LineAccessTokenResponse</returns>
        public async Task<(HttpStatusCode, LineAccessTokenResponse?)> GetAccessTokenAsync(LineAccessTokenRequest request)
#nullable disable
        {
            var message = new HttpRequestMessage(HttpMethod.Post, TokenBaseUri);
            var json = JsonConvert.SerializeObject(request);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            message.Content = new FormUrlEncodedContent(dictionary);

            using var client = new HttpClient();
            var response = await client.SendAsync(message);
            if (!response.IsSuccessStatusCode)
                return (response.StatusCode, null);

            var responseString = await response.Content.ReadAsStringAsync();
            return (response.StatusCode, JsonConvert.DeserializeObject<LineAccessTokenResponse>(responseString));
        }
    }
}
