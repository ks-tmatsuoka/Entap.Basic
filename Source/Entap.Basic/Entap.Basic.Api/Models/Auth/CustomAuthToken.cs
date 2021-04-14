using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    /// <summary>
    /// カスタム認証トークン
    /// AccessTokenとIDトークンのどちらか必須。
    /// </summary>
    public class CustomAuthToken
    {
        public CustomAuthToken()
        {
        }

        public CustomAuthToken(string accessToken, string idToken)
        {
            AccessToken = accessToken;
            IdToken = idToken;
        }

        /// <summary>
        /// アクセストークン
        /// </summary>
        /// <value>LINEのアクセストークン</value>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// IDトークン
        /// </summary>
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }
    }
}
