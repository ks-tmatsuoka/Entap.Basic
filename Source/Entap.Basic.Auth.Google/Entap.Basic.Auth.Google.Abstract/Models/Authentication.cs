using System;
namespace Entap.Basic.Auth.Google
{
    public class Authentication
    {
        public Authentication()
        {
        }

        /// <summary>
        /// アクセストークン（iOSのみ）
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// アクセストークン有効期限（iOSのみ）
        /// </summary>
        public DateTime AccessTokenExpirationDate { get; set; }

        /// <summary>
        /// IDトークン
        /// </summary>
        public string IdToken { get; set; }

        /// <summary>
        /// IDトークン有効期限（iOSのみ）
        /// </summary>
        public DateTime IdTokenExpirationDate { get; set; }

        /// <summary>
        /// リフレッシュトークン（iOSのみ）
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
