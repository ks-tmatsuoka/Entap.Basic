using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class BasicSendPasswordResetEmailPageUseCase : ISendPasswordResetEmailPageUseCase
    {
        public static readonly string RegexMailAddress = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        public BasicSendPasswordResetEmailPageUseCase()
        {
        }

        public virtual string ValidateMailAddress(string mailAddress)
        {
            if (string.IsNullOrEmpty(mailAddress)) return null;
            if (!Regex.IsMatch(mailAddress, RegexMailAddress)) return "形式不正";
            return null;
        }

        public virtual void SendPasswordResetEmail(string mailAddress)
        {
            ProcessManager.Current.Invoke(async () =>
            {
                try
                {
                    await BasicStartup.AuthService.SendPasswordResetEmailAsync(mailAddress);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
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
