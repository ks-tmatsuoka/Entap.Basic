using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Tasks;
using Firebase.DynamicLinks;
using Android.Content;

namespace SHIRO.CO.Droid
{
    [Activity(Label = "SHIRO.CO", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { "android.intent.action.VIEW" },
        Categories = new[]
        {
            "android.intent.category.DEFAULT",
            "android.intent.category.BROWSABLE"
        },
        DataScheme = "http", DataHost = "entapshiro.page.link"
        )
    ]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnSuccessListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Entap.Basic.Android.Platform.Init(this);
            Entap.Basic.Firebase.Auth.Android.Platform.Init(Application.Context, this, savedInstanceState);

            Firebase.DynamicLinks.FirebaseDynamicLinks.Instance.GetDynamicLink(this.Intent)
                .AddOnSuccessListener(this, this);

            // Facebook
            Plugin.FacebookClient.FacebookClientManager.Initialize(this);

            // Google
            Entap.Basic.Auth.Google.Android.Platform.Init(this, "716130417723-6gkc7ikv3vu0k5tne89t23jlosjb6dlj.apps.googleusercontent.com", RequestCodes.GoogleAuth);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Facebook
            Plugin.FacebookClient.FacebookClientManager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Xamarin.Essentials.Platform.OnResume();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            if (result is null) return;
            if (!(result is PendingDynamicLinkData linkData)) return;

            try
            {
                EmailLinkHandler.Current.HandleEmailAction(linkData.Link.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}