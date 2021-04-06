using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class BasicResetPasswordPageUseCase : IResetPasswordPageUseCase
    {
        // ToDo : BasicSignUpUseCaseと共有化
        public static readonly string RegexPassword = @"^[!-~]*$";

        public BasicResetPasswordPageUseCase()
        {
        }

        public virtual string ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;
            if (password.Length < 8) return "桁数不足";
            if (!Regex.IsMatch(password, RegexPassword)) return "形式不正";

            return null;
        }

        public void ResetPassword(string actionCode, string password)
        {
            BasicStartup.AuthManager.PasswordAuthService.ConfirmPasswordResetAsync(actionCode, password);
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
