using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public interface IResetPasswordPageUseCase : IPageLifeCycle
    {
        string ValidatePassword(string password);

        void ResetPassword(string actionCode, string password);
    }
}
