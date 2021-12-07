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
                NavigationControllerManager.NavigationController is null ||
                NavigationControllerManager.NavigationController.NavigationBar is null
            )
                return -1;

            return NavigationControllerManager.NavigationController.NavigationBar.Frame.Size.Height;
        }

        public double GetPageHeight()
        {
            return UIScreen.MainScreen.Bounds.Height - GetTopNavigationHeight() - GetStatusBarHeight();
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