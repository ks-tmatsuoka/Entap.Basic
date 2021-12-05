using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    public class DisplaySizeManager
    {
        public static Size ScreenSize
        {
            get
            {
                return new Size(DependencyService.Get<IGetDisplaySize>().GetWidth(), DependencyService.Get<IGetDisplaySize>().GetScreenHeight());
            }
        }

        public static Size PageSize
        {
            get
            {
                return new Size(DependencyService.Get<IGetDisplaySize>().GetWidth(), DependencyService.Get<IGetDisplaySize>().GetPageHeight());
            }
        }

        public static double StatusBarHeight
        {
            get
            {
                return DependencyService.Get<IGetDisplaySize>().GetStatusBarHeight();
            }
        }

        public static double Density
        {
            get
            {
                return DependencyService.Get<IGetDisplaySize>().GetDensity();
            }
        }

        public static double iOSNavigationBarHeight
        {
            get
            {
                return DependencyService.Get<IGetDisplaySize>().GetiOSNavigationBarHeight();
            }
        }

        public static double AndroidNavigationBarHeight
        {
            get
            {
                var a = DependencyService.Get<IGetDisplaySize>();
                return DependencyService.Get<IGetDisplaySize>().GetAndroidNavigationBarHeight();
            }
        }

        public static double AndroidTitleBarHeight
        {
            get
            {
                return DependencyService.Get<IGetDisplaySize>().GetAndroidTitleBarHeight();
            }
        }
    }
}
