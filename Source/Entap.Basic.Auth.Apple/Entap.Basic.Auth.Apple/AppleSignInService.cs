using System;
using System.Threading.Tasks;
using Entap.Basic.Auth.Apple.Abstract;

namespace Entap.Basic.Auth.Apple
{
    public class AppleSignInService : IAppleSignInService
    {
        public AppleSignInService()
        {
        }

#nullable enable
        public static void Init(params AuthorizationScope[]? scopes)
#nullable disable
        {
            throw new NotImplementedException();
        }

        public Task<AppleIdCredential> SignInAsync()
        {
            throw new NotImplementedException();
        }
    }
}
