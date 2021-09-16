using System;
namespace Entap.Basic.Auth.Apple.Abstract
{
    /// <summary>
    /// 認証スコープ
    /// https://developer.apple.com/documentation/authenticationservices/asauthorizationscope?language=objc
    /// </summary>
    public enum AuthorizationScope
	{
        FullName,
        Email
    }
}
