using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth.Line
{
    public interface ILineAuthService
    {
        Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes);
    }
}
