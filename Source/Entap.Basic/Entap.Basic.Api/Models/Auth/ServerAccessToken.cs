using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    public class ServerAccessToken
    {
        static readonly string DefaultTokenType = "Bearer";
        public ServerAccessToken()
        {
        }

        /// <summary>
        /// サーバーのアクセストークン
        /// </summary>
        /// <value>サーバーのアクセストークン</value>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// アクセストークンタイプ
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType
        {
            get => string.IsNullOrWhiteSpace(_tokenType) ?
                DefaultTokenType:
                _tokenType;
            set => _tokenType = value;
        }
        string _tokenType;
    }
}
