using System;
using System.Threading.Tasks;
using Entap.Basic.Auth.Apple.Abstract;

namespace Entap.Basic.Auth.Apple
{
    public class AppleSignInService : IAppleSignInService
    {
        public AppleSignInService(params AuthorizationScope[] scopes)
        {
            var _ = scopes;
        }

        public Task<AppleIdCredential> SignInAsync()
        {
            throw new NotImplementedException();
        }
    }
}
