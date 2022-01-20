using System;
namespace Entap.Basic.Auth.Line
{
    public class LoginResult
    {
        public LoginResult()
        {
        }

#nullable enable
        public UserProfile? UserProfile { get; internal set; }
#nullable disable
    }
}
