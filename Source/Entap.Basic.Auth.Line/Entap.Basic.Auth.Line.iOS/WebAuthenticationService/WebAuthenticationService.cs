using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(Entap.Basic.Auth.Line.iOS.WebAuthenticationService))]
namespace Entap.Basic.Auth.Line.iOS
{
    public class WebAuthenticationService : IWebAuthenticationService
    {
        public Task<WebAuthenticatorResult> AuthenticateAsync(Uri url, Uri callbaclUrl)
            => CustomWebAuthenticator.PlatformAuthenticateAsync(
                new WebAuthenticatorOptions { Url = url, CallbackUrl = callbaclUrl });

        public static bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
            => CustomWebAuthenticator.OpenUrl(new Uri(userActivity?.WebPageUrl?.AbsoluteString));
    }
}
