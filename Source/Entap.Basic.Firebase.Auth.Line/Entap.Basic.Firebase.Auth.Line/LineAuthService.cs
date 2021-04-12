using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;

namespace Entap.Basic.Firebase.Auth.Line
{
    public class LineAuthService : SnsAuthService, ISnsAuthService
    {
        public LineAuthService()
        {
        }

        public Task SignInAsync()
        {
            throw new NotImplementedException();
        }
    }
}
