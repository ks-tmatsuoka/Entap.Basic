using System;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public interface IPasswordSignInUseCase : IPageLifeCycle
    {
        string ValidateMailAddress(string mailAddress);
        string ValidatePassword(string password);

        void SignIn(string mailAddress, string password);
        void ResetPassword();
    }
}
