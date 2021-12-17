using System;
using System.Linq;
using Entap.Basic.Forms.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetDisplaySize_iOS))]
namespace Entap.Basic.Forms.iOS
{
    public class GetDisplaySize_iOS : IGetDisplaySize
    {
        public double GetAndroidNavigationBarHeight()
        {
            return -1;
        }

        public double GetDensity()
        {
            return UIScreen.MainScreen.Scale;
        }

        public double GetTopNavigationHeight()
        {
            if (
                NavigationControllerManager.CurrentNavigationController is null ||
                NavigationControllerManager.CurrentNavigationController.NavigationBar is null
            )
                return -1;

            return NavigationControllerManager.CurrentNavigationController.NavigationBar.Frame.Size.Height;
        }

        public double GetPageHeight()
        {
            var navgationBarHeight = GetTopNavigationHeight() < 0 ? 0 : GetTopNavigationHeight();
            return UIScreen.MainScreen.Bounds.Height - navgationBarHeight - GetStatusBarHeight();
        }

        public double GetScreenHeight()
        {
            return UIScreen.MainScreen.Bounds.Height;
        }

        public double GetStatusBarHeight()
        {
            return UIApplication.SharedApplication.StatusBarFrame.Size.Height;
        }

        public double GetWidth()
        {
            return UIScreen.MainScreen.Bounds.Width;
        }
    }
}