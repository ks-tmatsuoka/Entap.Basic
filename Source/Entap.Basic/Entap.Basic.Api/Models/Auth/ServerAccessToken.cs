using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    public class ServerAccessToken
    {
        public ServerAccessToken()
        {
        }

        /// <summary>
        /// サーバーのアクセストークン
        /// </summary>
        /// <value>サーバーのアクセストークン</value>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}
