using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;

namespace Entap.Basic.Core.Android
{
    /// <summary>
    /// プラットフォーム固有処理ハンドラー
    /// </summary>
    [Preserve(AllMembers = true)]
    public class PlatformHandler
    {
        public PlatformHandler()
        {
        }

        /// <summary>
        /// プラットフォーム固有処理を実行する
        /// </summary>
        /// <param name="activity">Activity</param>
        public static void Handle(Activity activity)
        {
            HandleAutofill(activity);
        }

        /// <summary>
        /// 自動入力の制御
        /// 自動入力により、Android 8.0、8.1でアプリがクラッシュ
        /// https://developer.android.com/guide/topics/text/autofill-optimize#autofill_causes_apps_to_crash_on_android_80_81
        /// </summary>
        public static void HandleAutofill(Activity activity)
        {
            if (Build.VERSION.SdkInt == BuildVersionCodes.O ||
                Build.VERSION.SdkInt == BuildVersionCodes.OMr1)
            {
                activity.Window.DecorView.ImportantForAutofill = ImportantForAutofill.NoExcludeDescendants;
            }
        }
    }
}
