using System;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SHIRO.CO.Droid
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
        Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
        DataScheme = "https", DataHost = "entapshiro.page.link", DataPath = "/auth_callback")]
    public class WebAuthenticationCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
    {
    }
}
