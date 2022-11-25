using System;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Entap.Basic.Settings;
using Xamarin.Forms;

namespace SHIRO.CO
{
    public class HomePageViewModel : PageViewModelBase
    {
        public HomePageViewModel()
        {
        }

        public ProcessCommand SettingsCommand => new ProcessCommand(async () =>
        {
            const string Logout = "ログアウト";
            const string LinkLine = "LINE連携";
            const string UnlinkLine = "LINE解除";
            const string LinkApple = "Apple連携";
            const string UnlinkApple = "Apple解除";
            const string LinkTwitter = "Twitter連携";
            const string UnlinkTwitter = "Twitter連携解除";
            const string LinkGoogle = "Google連携";
            const string UnlinkGoogle = "Google連携解除";
            const string LinkFacebook = "Facebook連携";
            const string UnlinkFacebook = "Facebook連携解除";
            var reult = await App.Current.MainPage.DisplayActionSheet("メニュー", "キャンセル", null, LinkLine, UnlinkLine, LinkApple, UnlinkApple, LinkTwitter, UnlinkTwitter, LinkGoogle, UnlinkGoogle, LinkFacebook, UnlinkFacebook, Logout);
            switch (reult)
            {
                case LinkLine:
                    await Entap.Basic.BasicStartup.AuthManager.LineAuthService.LinkAsync();
                    break;
                case UnlinkLine:
                    await Entap.Basic.BasicStartup.AuthManager.LineAuthService.UnlinkAsync();
                    break;
                case LinkApple:
                    await Entap.Basic.BasicStartup.AuthManager.AppleAuthService.LinkAsync();
                    break;
                case UnlinkApple:
                    await Entap.Basic.BasicStartup.AuthManager.AppleAuthService.UnlinkAsync();
                    break;
                case LinkTwitter:
                    await Entap.Basic.BasicStartup.AuthManager.TwitterAuthService.LinkAsync();
                    break;
                case UnlinkTwitter:
                    await Entap.Basic.BasicStartup.AuthManager.TwitterAuthService.UnlinkAsync();
                    break;
                case LinkGoogle:
                    await Entap.Basic.BasicStartup.AuthManager.GoogleAuthService.LinkAsync();
                    break;
                case UnlinkGoogle:
                    await Entap.Basic.BasicStartup.AuthManager.GoogleAuthService.UnlinkAsync();
                    break;
                case LinkFacebook:
                    await Entap.Basic.BasicStartup.AuthManager.FacebookAuthService.LinkAsync();
                    break;
                case UnlinkFacebook:
                    await Entap.Basic.BasicStartup.AuthManager.FacebookAuthService.UnlinkAsync();
                    break;
                case Logout:
                    await Entap.Basic.BasicStartup.AuthManager.SignOutAsync();
                    await Entap.Basic.BasicStartup.PageNavigator.SetGuidePageAsync();
                    break;
            }
        });
    }
}
