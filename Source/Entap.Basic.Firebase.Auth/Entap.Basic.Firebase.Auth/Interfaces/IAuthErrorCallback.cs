using System;
using System.Threading.Tasks;

namespace Entap.Basic.Firebase.Auth
{
    public interface IAuthErrorCallback
    {
        Task HandleSignInErrorAsync(Exception exception);
    }
}
