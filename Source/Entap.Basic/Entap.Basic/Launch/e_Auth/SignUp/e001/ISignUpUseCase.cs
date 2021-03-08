using System;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public interface ISignUpUseCase : IPageLifeCycle
    {
        string ValidateMailAddress(string mailAddress);
        string ValidatePassword(string password);

        void SignUp(string mailAddress, string password);
    }
}
