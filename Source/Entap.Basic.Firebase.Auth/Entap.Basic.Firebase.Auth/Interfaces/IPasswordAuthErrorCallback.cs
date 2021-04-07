using System;
using System.Threading.Tasks;

namespace Entap.Basic.Firebase.Auth
{
    public interface IPasswordAuthErrorCallback : IAuthErrorCallback
    {
        Task HandleSignUpErrorAsync(Exception exception);
        Task HandleSendPasswordResetEmailErrorAsync(Exception exception);
        Task HandleVerifyPasswordResetCodeErrorAsync(Exception exception);
        Task HandleConfirmPasswordResetErrorAsync(Exception exception);
    }
}
