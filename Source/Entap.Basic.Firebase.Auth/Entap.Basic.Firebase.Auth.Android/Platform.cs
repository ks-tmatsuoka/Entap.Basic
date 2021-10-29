using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Firebase;
using Plugin.CurrentActivity;

namespace Entap.Basic.Firebase.Auth.Android
{
    [Preserve(AllMembers = true)]
    public class Platform
    {
        public static void Init(Activity activity, Bundle bundle)
        {
            CrossCurrentActivity.Current.Init(activity, bundle);
            FirebaseApp.InitializeApp(activity);
        }
    }
}
