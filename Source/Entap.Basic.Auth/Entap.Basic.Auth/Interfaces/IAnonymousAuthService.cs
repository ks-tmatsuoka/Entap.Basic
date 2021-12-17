using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth
{
    public interface IAnonymousAuthService
    {
        Task SignInAsync();
    }
}
