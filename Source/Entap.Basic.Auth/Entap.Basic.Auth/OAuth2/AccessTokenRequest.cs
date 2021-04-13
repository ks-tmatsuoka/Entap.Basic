using System;
using Newtonsoft.Json;

namespace Entap.Basic.Auth.OAuth2
{
    /// <summary>
    /// アクセストークンリクエスト
    /// https://openid-foundation-japan.github.io/rfc6749.ja.html#token-req
    /// </summary>
    public class AccessTokenRequest
    {
        public AccessTokenRequest()
        {
        }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
