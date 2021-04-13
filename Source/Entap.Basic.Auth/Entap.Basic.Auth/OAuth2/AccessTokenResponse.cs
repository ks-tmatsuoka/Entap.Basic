using System;
using Newtonsoft.Json;

namespace Entap.Basic.Auth.OAuth2
{
    /// <summary>
    /// アクセストークンレスポンス
    /// https://openid-foundation-japan.github.io/rfc6749.ja.html#implicit-authz-resp
    /// </summary>
    public class AccessTokenResponse
    {
        public AccessTokenResponse()
        {
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("ExpiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public int Scope { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }
    }
}
