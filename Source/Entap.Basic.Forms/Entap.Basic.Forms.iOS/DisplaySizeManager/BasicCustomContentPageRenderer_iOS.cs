using System;
using Entap.Basic.Forms.iOS;
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

            NavigationControllerManager.NavigationController = NavigationController;
            var args = new iOSDisplaySizeRecivedEventArgs()
            {
                NavigationBarHeight = DisplaySizeManager.TopNavigationHeight,
                PageHeight = DisplaySizeManager.PageSize.Height
            };
            DisplaySizeManager.OniOSDisplaySizeReceiving(null, args);
        }
    }
}
