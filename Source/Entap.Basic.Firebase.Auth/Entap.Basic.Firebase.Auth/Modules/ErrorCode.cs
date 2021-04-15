using System;
namespace Entap.Basic.Firebase.Auth
{
    /// <summary>
    /// Firebase.AuthのErrorCodeを定義
    /// Plugin.FirebaseAuthでは、ErrorTypeに集約されたエラーをエラーコードによって判別可能とする
    /// https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
    /// https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/Android/ExceptionMapper.cs
    /// </summary>
    public struct ErrorCode
    {
        public const string UserDisabled = "ERROR_USER_DISABLED";
        public const string WebContextCancelled = "ERROR_WEB_CONTEXT_CANCELLED";
    }
}
