using System;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using LineSDKObjC;
using UIKit;

namespace Entap.Basic.Auth.Line
{
    public partial class LineAuthService : ILineAuthService
    {
        /// <summary>
        /// 初期化
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#call-loginmanagersetup-method
        /// </summary>
#nullable enable
        public static void Init(string channelId, string? universalLinkUrl)
#nullable disable
        {
            LineSDKLoginManager.SharedManager.SetupWithChannelID(channelId, new NSUrl(universalLinkUrl));
        }

        /// <summary>
        /// ContinueUserActivity
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#modify-app-delegate
        /// </summary>
        public static bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
            => LineSDKObjC.LineSDKLoginManager.SharedManager.Application(application, userActivity.WebPageUrl, new NSDictionary<NSString, NSObject>());

        /// <summary>
        /// OpenUrl
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-app-delegate
        /// </summary>
        public static bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
            => LineSDKObjC.LineSDKLoginManager.SharedManager.Application(app, url, new NSDictionary<NSString, NSObject>());

        /// <summary>
        /// OpenUrlContexts
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-scene-delegates
        /// </summary>
        public static void OpenUrlContexts(UIScene scene, NSSet<UIOpenUrlContext> urlContexts)
            => LineSDKObjC.LineSDKLoginManager.SharedManager.Application(UIApplication.SharedApplication, url: urlContexts?.ToArray()?.FirstOrDefault()?.Url, new NSDictionary<NSString, NSObject>());


        public Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes)
        {
            throw new NotImplementedException();
        }
    }
}
