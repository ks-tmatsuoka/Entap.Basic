using System;
namespace Entap.Basic.Firebase.Auth.EmailLink
{
    /// <summary>
    /// メールアクションパラメータ
    /// https://firebase.google.com/docs/auth/custom-email-handler#create_the_email_action_handler_page
    /// </summary>
    public class EmailActionParameter
    {
        public EmailActionParameter()
        {
        }

        /// <summary>
        /// Firebase プロジェクトの API キー
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// ユーザー管理アクション
        /// </summary>
        public string Mode { private get; set; }

        /// <summary>
        /// リクエストを識別し、検証するためのワンタイム コード
        /// </summary>
        public string OobCode { get; set; }

        /// <summary>
        /// URL を使用して状態をアプリに戻す方法を提供するオプションの URL
        /// </summary>
        public Uri ContinueUrl { get; set; }

        /// <summary>
        /// ユーザーのロケールを表す省略可能な BCP47 言語タグ
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// ユーザー管理アクション
        /// </summary>
        public EmailActionMode ActionMode =>
            Mode switch
            {
                "resetPassword" => EmailActionMode.ResetPassword,
                "recoverEmail" => EmailActionMode.RecoverEmail,
                "verifyEmail" => EmailActionMode.VerifyEmail,
                _ => EmailActionMode.Unknown,
            };
    }
}
