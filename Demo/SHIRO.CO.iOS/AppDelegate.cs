using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

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

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);

            // Facebook
            Plugin.FacebookClient.FacebookClientManager.OnActivated();
        }

        // iOS 9~
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            // Facebook
            Plugin.FacebookClient.FacebookClientManager.OpenUrl(app, url, options);

            // Google SignIn
            Google.SignIn.SignIn.SharedInstance.HandleUrl(url);
            return true;
        }

        // iOS ~8
        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // Facebook
            Plugin.FacebookClient.FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);

            // Google SignIn
            Google.SignIn.SignIn.SharedInstance.HandleUrl(url);
            return true;
        }
    }
}
