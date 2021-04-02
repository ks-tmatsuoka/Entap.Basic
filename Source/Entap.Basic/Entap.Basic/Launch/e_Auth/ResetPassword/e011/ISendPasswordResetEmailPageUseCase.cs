using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public interface ISendPasswordResetEmailPageUseCase : IPageLifeCycle
    {
        string ValidateMailAddress(string mailAddress);

        void SendPasswordResetEmail(string mailAddress);
    }
}
