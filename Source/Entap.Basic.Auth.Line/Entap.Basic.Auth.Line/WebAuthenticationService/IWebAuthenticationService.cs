using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Entap.Basic.Auth.Line
{
    public interface IWebAuthenticationService
    {
        Task<WebAuthenticatorResult> AuthenticateAsync(Uri url, Uri callbaclUrl);
    }
}
