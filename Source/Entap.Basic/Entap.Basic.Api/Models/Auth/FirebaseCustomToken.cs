using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    public class FirebaseCustomToken
    {
        public FirebaseCustomToken()
        {
        }

        /// <summary>
        /// カスタムトークン
        /// </summary>
        [JsonProperty(PropertyName = "custom_token")]
        public string CustomToken { get; set; }
    }
}
