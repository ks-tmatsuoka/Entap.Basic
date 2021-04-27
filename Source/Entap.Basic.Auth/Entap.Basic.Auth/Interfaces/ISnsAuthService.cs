using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth
{
    public interface ISnsAuthService
    {
        Task SignInAsync();
        Task SignOutAsync();
    }
}
