using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Entap.Basic.Forms
{
    /// <summary>
    /// Xamarin.Forms.Thicknessの拡張メソッド
    /// </summary>
    public static class ThicknessExtentions
    {
        /// <summary>
        /// SafeAreaを適用したThicknessを取得する
        /// </summary>
        /// <param name="thickness">SafeAreaを適用するThickness</param>
        /// <param name="positionFlags">SafeAreaを適用する位置</param>
        /// <returns>SafeAreaを適用したThickness</returns>
        public static Thickness? GetSafeAreaAppliedThickness(this Thickness thickness, ThicknessPositionFlags positionFlags)
        {
            if (positionFlags == 0) return null;
            if (Device.RuntimePlatform != Device.iOS) return null;

            var currentPage = PageManager.Navigation.GetCurrentPage();
            if (currentPage is null) return null;

            var safeAreaInsets = currentPage.On<iOS>().SafeAreaInsets();
            if (safeAreaInsets.IsEmpty) return null;

            var result = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
            if (positionFlags.HasFlag(ThicknessPositionFlags.Left))
                result.Left += safeAreaInsets.Left;

            if (positionFlags.HasFlag(ThicknessPositionFlags.Top))
                result.Top += safeAreaInsets.Top;

            if (positionFlags.HasFlag(ThicknessPositionFlags.Right))
                result.Right += safeAreaInsets.Right;

            if (positionFlags.HasFlag(ThicknessPositionFlags.Bottom))
                result.Bottom += safeAreaInsets.Bottom;

            return result;
        }
    }
}
