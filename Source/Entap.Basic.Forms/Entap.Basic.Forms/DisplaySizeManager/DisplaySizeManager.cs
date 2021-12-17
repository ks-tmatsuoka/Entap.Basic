using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    /// <summary>
    /// <para>注意</para>
    /// <para>iOSでナビゲーションバーない場合について</para>
    /// <para>TopNavigationHeightとPageSizeとiOSDisplaySizeRecivedのNavigationBarHeight、PageHeightは、HasNavigationBar="False"でナビゲーション使ってない場合でもナビゲーションバーがある際のサイズを返すので、ナビゲーションバーのないページでページのサイズ取得したい場合PageSizeのHeightにTopNavigationHeightを足した数値が正確なPageSizeとなります。PageManager.SetMainPageでナビゲーション使ってない場合はPageSizeのHeightにTopNavigationHeightを足す必要はないです。 </para>
    /// <para>Androidでナビゲーションバーない場合について</para>
    /// <para>TopNavigationHeightとPageSizeとiOSDisplaySizeRecivedのNavigationBarHeight、PageHeightは、HasNavigationBar="False"およびSetMainPageでナビゲーション使ってない場合でもナビゲーションバーがある際のサイズを返すので、ナビゲーションバーのないページでページのサイズ取得したい場合PageSizeのHeightにTopNavigationHeightを足した数値が正確なPageSizeとなります。</para>
    /// <para>iOS Androidともにナビゲーションバーがない場合のPageの高さはScreenSize - StatusBarHeight で取得できます。</para>
    /// <para>TopNavigationHeightとPageSizeとiOSDisplaySizeRecivedのNavigationBarHeight、PageHeightは、HasNavigationBar="False"およびSetMainPageでナビゲーション使ってない場合でもナビゲーションバーがある際のサイズを返すので、ナビゲーションバーのないページでページのサイズ取得したい場合PageSizeのHeightにTopNavigationHeightを足した数値が正確なPageSizeとなります。</para>
    /// </summary>
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

        /// <summary>
        /// 画面のサイズ(ナビゲーションバーやステータスバーの高さを含む)
        /// </summary>
        public static Size ScreenSize
        {
            get
            {
                return new Size(getGetDisplaySizeInstanse.GetWidth(), getGetDisplaySizeInstanse.GetScreenHeight());
            }
        }

        /// <summary>
        /// <para>ページのサイズ(ナビゲーションバーやステータスバーの高さは含まない)</para>
        /// <para>注意</para>
        /// <para>iOSの場合、各ページのコードビハインドやViewModelのコンストラクタ内では使用せず、iOSDisplaySizeChangedの方を使う</para>
        /// </summary>
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

        /// <summary>
        /// <para>注意</para>
        /// <para>iOSの場合、各ページのコードビハインドやViewModelのコンストラクタ内でナビゲーションバーなどの高さが欲しければこれを使用する</para>
        /// </summary>
        public static event EventHandler<iOSDisplaySizeRecivedEventArgs> iOSDisplaySizeChanged;
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal static void OniOSDisplaySizeChanging(object obj, iOSDisplaySizeRecivedEventArgs args)
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

        /// <summary>
        /// <para>注意</para>
        /// <para>iOSの場合、各ページのコードビハインドやViewModelのコンストラクタ内では使用せず、iOSDisplaySizeChangedの方を使う</para>
        /// </summary>
        public static double TopNavigationHeight
        {
            get
            {
                return getGetDisplaySizeInstanse.GetTopNavigationHeight();
            }
        }
    }
}
