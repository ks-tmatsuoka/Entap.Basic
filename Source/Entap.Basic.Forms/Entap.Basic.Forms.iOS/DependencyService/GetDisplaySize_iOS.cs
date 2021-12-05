using System;
using Entap.Basic.Forms.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(GetDisplaySize_iOS))]
namespace Entap.Basic.Forms.iOS
{
    public class GetDisplaySize_iOS : IGetDisplaySize
    {
        public double GetAndroidNavigationBarHeight()
        {
            return 1;
        }

        public double GetAndroidTitleBarHeight()
        {
            return 1;
        }

        public double GetDensity()
        {
            return 1;
        }

        public double GetiOSNavigationBarHeight()
        {
            return 1;
        }

        public double GetPageHeight()
        {
            return 1;
        }

        public double GetScreenHeight()
        {
            return 1;
        }

        public double GetStatusBarHeight()
        {
            return 1;
        }

        public double GetWidth()
        {
            return 1;
        }
    }
}