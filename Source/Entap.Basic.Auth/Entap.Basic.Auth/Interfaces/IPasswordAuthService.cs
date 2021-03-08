using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth.Abstractions
{
    public interface IPasswordAuthService
    {
        #nullable enable
        Task<string?> SignUpAsync(string email, string password);
        Task<string?> SignInAsync(string email, string password);
        #nullable disable
        void SignOut();
    }
}
