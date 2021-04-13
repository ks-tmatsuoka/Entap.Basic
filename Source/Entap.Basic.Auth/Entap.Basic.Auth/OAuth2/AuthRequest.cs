using System;
namespace Entap.Basic.Auth.OAuth2
{
    /// <summary>
    /// 認可リクエスト
    /// https://openid-foundation-japan.github.io/rfc6749.ja.html#implicit-authz-req
    /// </summary>
    public class AuthRequest
    {
        public AuthRequest(string responseType, string clientId, string redirectUri, string scope = null, string state = null)
        {
            ResponseType = responseType;
            ClientId = clientId;
            RedirectUri = redirectUri;
            Scope = scope;
            State = state;
        }

        /// <summary>
        /// （必須）リクエストタイプ
        /// </summary>
        public string ResponseType { get; set; }

        /// <summary>
        /// （必須）クライアントID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// （任意）リダイレクトURI
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// （推奨）アクセストークンのスコープ
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// （推奨）リクエストとコールバックの間で状態を維持するために使用するランダムな値
        /// </summary>
        public string State { get; set; }
    }
}
