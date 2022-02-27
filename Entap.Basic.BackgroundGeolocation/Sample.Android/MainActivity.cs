using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Entap.Basic.BackgroundGeolocation;
using Android.Content;

namespace LRMS.Droid
{
    [Activity(Label = "LRMS", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Entap.Basic.Forms.Android.Platform.Init(this);
            ConfigureServices();
            LoadApplication(new App());
            
            // RequestIgnoreBatteryOptimizations()
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void ConfigureServices()
        {
            Entap.Basic.Core.BasicStartup.Current.ConfigureGeolocationNotificationProvider<GeolocationNotificationProvider>();
        }

        void RequestIgnoreBatteryOptimizations()
        {
            var intent = new Intent(Android.Provider.Settings.ActionRequestIgnoreBatteryOptimizations);
            intent.SetData(Android.Net.Uri.Parse("package:" + Xamarin.Essentials.AppInfo.PackageName));
            StartActivity(intent);
        }
    }
}
