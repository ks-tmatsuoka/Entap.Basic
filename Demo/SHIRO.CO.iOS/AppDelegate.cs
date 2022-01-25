using System;
using System.Collections.Generic;
using System.Linq;
using Entap.Basic.Auth.Apple;
using Firebase.DynamicLinks;
using Foundation;
using Plugin.FirebaseAuth;
using UIKit;
using Xamarin.Essentials;

namespace SHIRO.CO.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Entap.Basic.iOS.Platform.Init();
            Entap.Basic.Firebase.Auth.iOS.Platform.Init();

            // Facebook
            Plugin.FacebookClient.FacebookClientManager.Initialize(app, options);
            // Google
            Entap.Basic.Auth.Google.iOS.Platform.Init(
                Firebase.Core.Options.DefaultInstance.ClientId,
                Xamarin.Essentials.Platform.GetCurrentUIViewController);

            Entap.Basic.Auth.Line.LineAuthService.Init("1655277852");

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                Entap.Basic.Auth.Apple.AppleSignInService.Init();
                Entap.Basic.Auth.Apple.Forms.iOS.Platform.Init();
            }

            LoadApplication(new App());
            AppleSignInService.RegisterCredentialRevokedActionAsync(null, async () =>
            {
                if (CrossFirebaseAuth.Current.Instance.CurrentUser is null)
                    return;

                await Entap.Basic.BasicStartup.AuthManager.SignOutAsync();
            }).ContinueWith((arg) =>
            {

                if (arg.IsFaulted)
                {
                    // ToDo :  エラー処理
                }
            }).ConfigureAwait(false);
#if DEBUG
            DynamicLinks.PerformDiagnostics(null);
#endif
            return base.FinishedLaunching(app, options);
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            if (Entap.Basic.Auth.Line.LineAuthService.ContinueUserActivity(application, userActivity, completionHandler))
                return true;

            if (Platform.ContinueUserActivity(application, userActivity, completionHandler))
                return true;

            var handled = DynamicLinks.SharedInstance.HandleUniversalLink(userActivity.WebPageUrl, (dynamicLink, error) =>
            {
            });
            return handled;
        }

        // iOS 9~
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            // Facebook
            Plugin.FacebookClient.FacebookClientManager.OpenUrl(app, url, options);

            // Google SignIn
            Entap.Basic.Auth.Google.GoogleAuthService.OnOpenUrl(app, url, options);

            if (Entap.Basic.Auth.Line.LineAuthService.OpenUrl(app, url, options))
                return true;

            var dynamicLink = DynamicLinks.SharedInstance?.FromCustomSchemeUrl(url);
            if (dynamicLink?.Url is null) return false;

            EmailLinkHandler.Current.HandleEmailAction(dynamicLink.Url.ToString());
            return true;
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);

            // Facebook
            Plugin.FacebookClient.FacebookClientManager.OnActivated();
        }
    }
}
