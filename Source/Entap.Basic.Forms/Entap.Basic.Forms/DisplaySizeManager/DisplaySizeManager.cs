using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    public class DisplaySizeManager
    {
        // 注意
        // iOSでナビゲーションバーない場合について
        // TopNavigationHeightとPageSizeとiOSDisplaySizeRecivedのNavigationBarHeight、PageHeightは、
        // HasNavigationBar="False"でナビゲーション使ってない場合でもナビゲーションバーがある際のサイズを返すので、
        // ナビゲーションバーのないページでページのサイズ取得したい場合PageSizeのHeightにTopNavigationHeightを足した数値が正確なPageSizeとなります。
        //　PageManager.SetMainPageでナビゲーション使ってない場合はPageSizeのHeightにTopNavigationHeightを足す必要はないです。
        //
        // androidでナビゲーションバーない場合について
        // TopNavigationHeightとPageSizeとiOSDisplaySizeRecivedのNavigationBarHeight、PageHeightは、
        // HasNavigationBar="False"およびSetMainPageでナビゲーション使ってない場合でもナビゲーションバーがある際のサイズを返すので、
        // ナビゲーションバーのないページでページのサイズ取得したい場合PageSizeのHeightにTopNavigationHeightを足した数値が正確なPageSizeとなります。

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
        public static event EventHandler<iOSDisplaySizeRecivedEventArgs> iOSDisplaySizeChanged;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void OniOSDisplaySizeChanging(object obj, iOSDisplaySizeRecivedEventArgs args)
        {
            EventHandler<iOSDisplaySizeRecivedEventArgs> handler = iOSDisplaySizeChanged;
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
