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

        #region PasswordAuth
        public bool IsPasswordAuthSupported => PasswordAuthService is not null;
        public IPasswordAuthService PasswordAuthService => _passwordAuthService ??= new PasswordAuthService();
        IPasswordAuthService _passwordAuthService;
        #endregion

        #region TwitterAuth
        public bool IsTwitterAuthSupported => TwitterAuthService is not null;
        public ISnsAuthService TwitterAuthService => _twitterAuthService ??= new TwitterAuthService();
        ISnsAuthService _twitterAuthService;
        #endregion


    }
}
