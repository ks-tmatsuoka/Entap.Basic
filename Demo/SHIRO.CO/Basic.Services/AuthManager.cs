using System;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Firebase.Auth;

namespace SHIRO.CO
{
    public class AuthManager : IAuthManager
    {
        public AuthManager()
        {
        }

        public bool IsPasswordAuthSupported => PasswordAuthService is not null;
        public IPasswordAuthService PasswordAuthService => _passwordAuthService ??= new PasswordAuthService();
        IPasswordAuthService _passwordAuthService;
    }
}
