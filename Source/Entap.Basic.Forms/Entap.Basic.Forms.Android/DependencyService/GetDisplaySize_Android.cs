using System;
using Android.Graphics;
using Android.Util;
using Entap.Basic.Forms.Android;
using Xamarin.Forms;
using Android.App;
using Android.Views;
using Xamarin.Forms.Platform.Android.AppCompat;
using System.Reflection;
using Android.Content.Res;
using Android.Widget;

[assembly: Dependency(typeof(GetDisplaySize_Android))]
namespace Entap.Basic.Forms.Android
{
    public class GetDisplaySize_Android : IGetDisplaySize
    {
        public double GetAndroidNavigationBarHeight()
        {
            return GetScreenHeight() - GetMetricsHeight();
        }

        public double GetAndroidTitleBarHeight()
        {
            double defaultSize = 56;
            var activity = Platform.GetActivity();

            //Xamarin.Forms.Application.Current.MainPage.

            //var toolbar = activity.FindViewById(Resource.Id.action_bar);
            //var aa = toolbar.Height;
            //int layoutWidth = layout.getWidth();
            //int layoutHeight = layout.getHeight();


            TypedValue tv = new TypedValue();
            if (activity.Theme.ResolveAttribute(Resource.Attribute.actionBarSize, tv, true))
            {
                var actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, activity.ApplicationContext.Resources.DisplayMetrics);
                return (double)actionBarHeight / GetDensity();
            }


            return defaultSize;
        }

        public double GetDensity()
        {
            var activity = Platform.GetActivity();
            DisplayMetrics metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            return metrics.Density;
        }

        public double GetWidth()
        {
            var activity = Platform.GetActivity();
            DisplayMetrics metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            return (double)metrics.WidthPixels / (double)metrics.Density;
        }

        public double GetiOSNavigationBarHeight()
        {
            return 0;
        }

        public double GetPageHeight()
        {
            return GetMetricsHeight() - GetAndroidNavigationBarHeight() - GetAndroidTitleBarHeight();
        }

        public double GetScreenHeight()
        {
            var activity = Platform.GetActivity();
            DisplayMetrics metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetRealMetrics(metrics);
            return (double)metrics.HeightPixels / (double)metrics.Density;
        }

        public double GetStatusBarHeight()
        {
            var activity = Platform.GetActivity();
            var context = activity.ApplicationContext;
            if (activity.ApplicationContext != null)
            {
                try
                {
                    var resources = context.Resources;
                    var resourceId = resources.GetIdentifier("status_bar_height", "dimen", "android");
                    if (resourceId > 0)
                    {
                        var metrics = resources.DisplayMetrics;
                        // ステータスバーのHeight
                        return resources.GetDimensionPixelSize(resourceId) / metrics.Density;
                    }
                }
                catch(Exception e)
                {

                }
                
            }
            // onCreateで呼び出すとまだViewの計算が終わってないので0になってしまう
            Rect rect = new Rect();
            activity.Window.DecorView.GetWindowVisibleDisplayFrame(rect);
            return rect.Top;
        }

        double GetMetricsHeight()
        {
            var activity = Platform.GetActivity();
            DisplayMetrics metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            return (double)metrics.HeightPixels / (double)metrics.Density;
        }
    }
}
