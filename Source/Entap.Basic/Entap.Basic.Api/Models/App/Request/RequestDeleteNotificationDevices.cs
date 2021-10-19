using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    public class RequestDeleteNotificationDevices
    {
        public RequestDeleteNotificationDevices(string token)
        {
            Token = token;
        }

        /// <summary>
        /// 削除対象のデバイストークン
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
