using System;
namespace Entap.Basic.Auth.OAuth2
{
    /// <summary>
    /// 認可レスポンス
    /// https://openid-foundation-japan.github.io/rfc6749.ja.html#code-authz-resp
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// 認可コード
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// リクエストとコールバックの間で状態を維持するために使用するランダムな値
        /// </summary>
        public string State { get; set; }
    }
}
