using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth
{
    public interface IGoogleAuthService : ISnsAuthService
    {
        Task SignOutAsync();
    }
}
