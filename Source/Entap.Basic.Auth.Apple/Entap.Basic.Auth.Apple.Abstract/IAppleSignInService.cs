using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth.Apple.Abstract
{
    public interface IAppleSignInService
    {
        public Task<AppleIdCredential> SignInAsync();
    }
}
