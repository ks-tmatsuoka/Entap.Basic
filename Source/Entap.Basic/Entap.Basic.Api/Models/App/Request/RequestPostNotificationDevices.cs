using System;
using Newtonsoft.Json;

namespace Entap.Basic.Api
{
    public class RequestPostNotificationDevices
    {
        public RequestPostNotificationDevices(string token)
        {
            Token = token;
        }

        /// <summary>
        /// 通知先のトークン
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
