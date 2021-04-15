using System;
using Entap.Basic.Auth.OAuth2;
using Newtonsoft.Json;

namespace Entap.Basic.Auth.Line
{
    /// <summary>
    /// 認可リクエスト
    /// https://developers.line.biz/ja/docs/line-login/integrate-line-login/#making-an-authorization-request
    /// </summary>
    public class LineAuthRequest : AuthRequest
    {
        const string CodeResponseType = "code";
        public LineAuthRequest(string clientId, string redirectUri, string scope = null, string state = null) : base (CodeResponseType, clientId, redirectUri, scope, state)
        {
        }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("max_age")]
        public int? MaxAge { get; set; }

        [JsonProperty("ui_locales")]
        public string UiLocales { get; set; }

        [JsonProperty("bot_prompt")]
        public string BotPrompt { get; set; }

        [JsonProperty("initial_amr_display")]
        public string InitialAmrDisplay { get; set; }

        [JsonProperty("switch_amr")]
        public bool? SwitchAmr { get; set; }

        [JsonProperty("disable_ios_auto_login")]
        public bool? DisableiOSAutoLogin { get; set; }

        [JsonProperty("code_challenge")]
        public string CodeChallenge { get; set; }

        [JsonProperty("code_challenge_method")]
        public string CodeChallengeMethod { get; set; }
    }
}
