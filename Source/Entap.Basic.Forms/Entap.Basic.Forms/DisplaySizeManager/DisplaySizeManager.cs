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

        // iOSの場合、各ページのコードビハインドやViewModelのコンストラクタ内では使用せず、iOSDisplaySizeRecivedの方を使う
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

        // 各ページのコードビハインドやViewModelのコンストラクタ内では使用せず、iOSDisplaySizeRecivedの方を使う
        public static double iOSNavigationBarHeight
        {
            get
            {
                return DependencyService.Get<IGetDisplaySize>().GetiOSNavigationBarHeight();
            }
        }

        // iOSの場合、各ページのコードビハインドやViewModelのコンストラクタ内でナビゲーションバーなどの高さが欲しければこれを使用する
        public static event EventHandler<iOSDisplaySizeRecivedEventArgs> iOSDisplaySizeRecived;
        public static void OniOSDisplaySizeReceiving(object obj, iOSDisplaySizeRecivedEventArgs args)
        {
            EventHandler<iOSDisplaySizeRecivedEventArgs> handler = iOSDisplaySizeRecived;
            if (handler != null)
            {
                handler.Invoke(obj, args);
            }
        }

        public static double AndroidNavigationBarHeight
        {
            get
            {
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
