using System;
using Entap.Basic.Auth.OAuth2;
using Newtonsoft.Json;

namespace Entap.Basic.Auth.Line
{
    /// <summary>
    /// 認可レスポンス
    /// https://developers.line.biz/ja/docs/line-login/integrate-line-login/#receiving-the-authorization-code
    /// </summary>
    public class LineAuthResponse : AuthResponse
    {
        public LineAuthResponse()
        {
        }

        [JsonProperty("friendship_status_changed")]
        public bool? FriendshipStatusChanged { get; set; }

        #region Error
        // https://developers.line.biz/ja/docs/line-login/integrate-line-login/#receiving-an-error-response

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        public bool IsError => !string.IsNullOrEmpty(Error);
        #endregion
    }
}
