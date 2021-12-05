using System;
namespace Entap.Basic.Forms
{
    public interface IGetDisplaySize
    {
        double GetWidth();
        double GetScreenHeight();
        double GetPageHeight();
        double GetDensity();
        double GetStatusBarHeight();
        double GetiOSNavigationBarHeight();
        double GetAndroidNavigationBarHeight();
        double GetAndroidTitleBarHeight();
    }
}
