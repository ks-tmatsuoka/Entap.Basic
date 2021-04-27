using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth.Google
{
    public interface IGoogleAuthService
    {
        Task<Authentication> AuthAsync();
        Task SignOutAsync();
    }
}
