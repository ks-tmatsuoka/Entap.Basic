using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    /// <summary>
    /// FirebaseのIDトークン
    /// </summary>
    public class FirebaseIdToken
    {
        public FirebaseIdToken(string idToken)
        {
            IdToken = idToken;
        }

        /// <summary>
        /// FirebaseのIDトークン
        /// </summary>
        /// <value>FirebaseのIDトークン</value>
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }
    }
}
