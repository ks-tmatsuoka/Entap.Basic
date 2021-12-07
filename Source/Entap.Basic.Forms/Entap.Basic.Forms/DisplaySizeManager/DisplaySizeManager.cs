using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    public class DisplaySizeManager
    {
        static IGetDisplaySize getGetDisplaySize;
        static IGetDisplaySize getGetDisplaySizeInstanse
        {
            get
            {
                if (getGetDisplaySize is null)
                    getGetDisplaySize = DependencyService.Get<IGetDisplaySize>();
                return getGetDisplaySize;
            }
        }
        public static Size ScreenSize
        {
            get
            {
                return new Size(getGetDisplaySizeInstanse.GetWidth(), getGetDisplaySizeInstanse.GetScreenHeight());
            }
        }

        // iOSの場合、各ページのコードビハインドやViewModelのコンストラクタ内では使用せず、iOSDisplaySizeRecivedの方を使う
        public static Size PageSize
        {
            get
            {
                return new Size(getGetDisplaySizeInstanse.GetWidth(), getGetDisplaySizeInstanse.GetPageHeight());
            }
        }

        public static double StatusBarHeight
        {
            get
            {
                return getGetDisplaySizeInstanse.GetStatusBarHeight();
            }
        }

        public static double Density
        {
            get
            {
                return getGetDisplaySizeInstanse.GetDensity();
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

        public static double AndroidBottomNavigationBarHeight
        {
            get
            {
                return getGetDisplaySizeInstanse.GetAndroidNavigationBarHeight();
            }
        }

        // iOSでは各ページのコードビハインドやViewModelのコンストラクタ内では使用せず、iOSDisplaySizeRecivedの方を使う
        public static double TopNavigationHeight
        {
            get
            {
                return getGetDisplaySizeInstanse.GetTopNavigationHeight();
            }
        }
    }
}
