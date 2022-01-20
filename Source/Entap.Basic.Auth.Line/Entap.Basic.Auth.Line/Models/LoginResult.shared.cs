using System;
namespace Entap.Basic.Auth.Line
{
    public class LoginResult
    {
        public LoginResult()
        {
        }

        public LineAccessTokenResponse LineAccessToken { get; internal set; }

#nullable enable
        public UserProfile? UserProfile { get; internal set; }
#nullable disable
    }
}
