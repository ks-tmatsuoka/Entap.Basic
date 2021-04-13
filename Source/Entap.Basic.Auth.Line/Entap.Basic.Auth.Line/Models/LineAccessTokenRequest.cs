using System;
using Entap.Basic.Auth.OAuth2;
using Newtonsoft.Json;

namespace Entap.Basic.Auth.Line
{
    /// <summary>
    ///
    /// https://developers.line.biz/ja/reference/line-login/#issue-token-request-body
    /// </summary>
    public class LineAccessTokenRequest : AccessTokenRequest
    {
        public LineAccessTokenRequest(string code, string redirectUri, string clientId, string clientSecret, string codeVerifier = null)
            : base ("authorization_code", code, redirectUri, clientId)
        {
            ClientSecret = clientSecret;
            CodeVerifier = codeVerifier;
        }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("code_verifier")]
        public string CodeVerifier { get; set; }
    }
}
