using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Entap.Basic.Api
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        /// <summary>
        /// メッセージ
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// エラー情報
        /// Errors内のプロパティが可変なので、JObjectとして定義
        /// </summary>
        [JsonProperty("errors")]
        public JObject Errors { get; set; }
    }
}
