using System;
namespace Entap.Basic.Auth.Apple.Abstract
{
    /// <summary>
    /// ユーザ検出ステータス
    /// https://developer.apple.com/documentation/authenticationservices/asuserdetectionstatus
    /// </summary>
    public enum UserDetectionStatus
    {
        Unsupported,
        Unknown,
        LikelyReal
    }
}
