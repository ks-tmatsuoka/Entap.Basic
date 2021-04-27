using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth.Google
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public GoogleAuthService()
        {
        }

        public Task<Authentication> AuthAsync()
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
