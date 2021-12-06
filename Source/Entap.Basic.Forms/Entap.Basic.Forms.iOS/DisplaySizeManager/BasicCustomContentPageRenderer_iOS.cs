using System;
using System.Linq;
using Entap.Basic.Forms.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(BasicCustomContentPageRenderer_iOS))]
namespace Entap.Basic.Forms.iOS
{
    public class BasicCustomContentPageRenderer_iOS : PageRenderer
    {
        public BasicCustomContentPageRenderer_iOS()
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (NavigationController != null || NavigationController?.NavigationBar != null)
            {
                var args = new iOSDisplaySizeRecivedEventArgs()
                {
                    NavigationBarHeight = NavigationController.NavigationBar.Frame.Size.Height,
                    PageHeight = UIScreen.MainScreen.Bounds.Height - NavigationController.NavigationBar.Frame.Size.Height - DisplaySizeManager.StatusBarHeight
                };
                DisplaySizeManager.OniOSDisplaySizeReceiving(null, args);
                NavigationControllerManager.NavigationController = NavigationController;
            }
            else
            {
                var args = new iOSDisplaySizeRecivedEventArgs() { NavigationBarHeight = 0, PageHeight = UIScreen.MainScreen.Bounds.Height - DisplaySizeManager.StatusBarHeight};
                DisplaySizeManager.OniOSDisplaySizeReceiving(null, args);
                NavigationControllerManager.NavigationController = null;
            }
        }
    }
}
