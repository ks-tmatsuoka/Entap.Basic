using System;
using System.Text.RegularExpressions;
using Entap.Basic.Core;

namespace Entap.Basic.Launch.Auth
{
    public class BasicSignUpPageUseCase : ISignUpPageUseCase
    {
        public static readonly string RegexMailAddress = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        public static readonly string RegexPassword = @"^[!-~]*$";

        public BasicSignUpPageUseCase()
        {
        }

        public virtual string ValidateMailAddress(string mailAddress)
        {
            if (string.IsNullOrEmpty(mailAddress)) return null;
            if (!Regex.IsMatch(mailAddress, RegexMailAddress)) return "形式不正";
            return null;
        }

        public virtual string ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;
            if (password.Length < 8) return "桁数不足";
            if (!Regex.IsMatch(password, RegexPassword)) return "形式不正";

            return null;
        }

        public virtual void SignUp(string mailAddress, string passwrod)
        {
            ProcessManager.Current.Invoke(async () =>
            {
                var token = await BasicStartup.AuthManager.PasswordAuthService.SignUpAsync(mailAddress, passwrod);
                if (token is null) return;

                // ToDo ページ遷移
            });
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
