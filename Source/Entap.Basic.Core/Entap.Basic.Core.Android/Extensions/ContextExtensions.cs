using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace Entap.Basic.Core.Android
{
    public static class ContextExtensions
    {
        /// <summary>
        /// ForegroundServiceを起動する
        /// https://docs.microsoft.com/ja-jp/xamarin/android/app-fundamentals/services/foreground-services
        /// </summary>
        public static void StartForegroundServiceCompat<T>(this Context context, Bundle args = null) where T : Service
        {
            var intent = new Intent(context, typeof(T));
            if (args != null)
            {
                intent.PutExtras(args);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                context.StartForegroundService(intent);
            }
            else
            {
                context.StartService(intent);
            }
        }
    }
}
