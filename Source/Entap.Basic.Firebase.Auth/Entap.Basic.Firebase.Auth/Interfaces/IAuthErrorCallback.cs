using System;
using System.Threading.Tasks;

namespace Entap.Basic.Firebase.Auth
{
    public interface IAuthErrorCallback
    {
        Task HandleSignInErrorAsync(Exception exception);
        Task HandleSignOutErrorAsync(Exception exception);

        Task HandleLinkErrorAsync(Exception exception);
        Task HandleUnlinkErrorAsync(Exception exception);
    }
}
