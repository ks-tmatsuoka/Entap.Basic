using System;
using Xamarin.Essentials;

namespace LRMS
{
    public static class PreferencesService
    {
        struct Keys
        {
            public const string IsGeolocationListening = "IsGeolocationListening";
        }

        /// <summary>
        /// 位置情報の取得状況を設定
        /// </summary>
        /// <param name="isListening"><c>ture</c>:取得中, <c>ture</c>非取得中</param>
        public static void SetIsGeolocationListening(bool isListening)
            => Preferences.Set(Keys.IsGeolocationListening, isListening);

        /// <summary>
        /// 位置情報の取得状況を取得
        /// </summary>
        /// <returns><c>ture</c>:取得中, <c>ture</c>非取得中</returns>
        public static bool GetIsGeolocationListening()
            => Preferences.Get(Keys.IsGeolocationListening, false);
    }
}
