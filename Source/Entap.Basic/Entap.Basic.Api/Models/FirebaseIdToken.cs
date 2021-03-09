using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    /// <summary>
    /// FirebaseのIDトークン
    /// </summary>
    public class FirebaseIdToken
    {
        /// <summary>
        /// FirebaseのIDトークン
        /// </summary>
        /// <value>FirebaseのIDトークン</value>
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }
    }
}
