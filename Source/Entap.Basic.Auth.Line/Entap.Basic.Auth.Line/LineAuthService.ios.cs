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
        public static void Init(string channelId, string? universalLinkUrl = null)
#nullable disable
        {
            var url = string.IsNullOrEmpty(universalLinkUrl) ? null : new NSUrl(universalLinkUrl);
            LineSDKLoginManager.SharedManager.SetupWithChannelID(channelId, url);
        }

        /// <summary>
        /// ContinueUserActivity
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#modify-app-delegate
        /// </summary>
        public static bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
            => LineSDKLoginManager.SharedManager.Application(application, userActivity.WebPageUrl, new NSDictionary<NSString, NSObject>());

        /// <summary>
        /// OpenUrl
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-app-delegate
        /// </summary>
        public static bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
            => LineSDKLoginManager.SharedManager.Application(app, url, new NSDictionary<NSString, NSObject>());

        /// <summary>
        /// OpenUrlContexts
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-scene-delegates
        /// </summary>
        public static void OpenUrlContexts(UIScene scene, NSSet<UIOpenUrlContext> urlContexts)
            => LineSDKLoginManager.SharedManager.Application(UIApplication.SharedApplication, url: urlContexts?.ToArray()?.FirstOrDefault()?.Url, new NSDictionary<NSString, NSObject>());

        /// <summary>
        /// ログイン処理
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#use-code
        /// </summary>
        public Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes)
        {
            var loginPermission = scopes
                .Select((LoginScope scope) => GetLoginPermission(scope))
                .ToArray();
            var permissions = new NSSet<LineSDKLoginPermission>(loginPermission);
            var viewController = Xamarin.Essentials.Platform.GetCurrentUIViewController();

            TaskCompletionSource<LoginResult> tcs = new TaskCompletionSource<LoginResult>();
            LineSDKLoginManager.SharedManager.LoginWithPermissions(permissions, viewController, (LineSDKLoginResult arg1, NSError arg2) =>
            {
                if (arg2 is null)
                {
                    tcs.TrySetResult(GetLoginResult(arg1));
                }
                else
                {
                    // ToDo : キャンセル判定
                    tcs.TrySetException(new NSErrorException(arg2));
                }
            });
            return tcs.Task;
        }

        LineSDKLoginPermission GetLoginPermission(LoginScope scope)
        {
            return scope switch
            {
                LoginScope.OpenID => LineSDKLoginPermission.OpenID,
                LoginScope.Profile => LineSDKLoginPermission.Profile,
                LoginScope.Email => LineSDKLoginPermission.Email,
                _ => throw new ArgumentOutOfRangeException(nameof(LoginScope)),
            };
        }

        LoginResult GetLoginResult(LineSDKLoginResult arg1)
        {
            var accessToken = GetAccessToken(arg1.AccessToken);
            var userProfile = GetUserProfile(arg1);
            return new LoginResult
            {
                LineAccessToken = new LineAccessTokenResponse
                {
                    AccessToken = accessToken.AccessTokenAccessToken,
                    TokenType = accessToken.TokenType,
                    ExpiresIn = accessToken.ExpiresIn,
                    Scope = accessToken.Scope,
                    IdToken = accessToken.IdToken,
                    RefreshToken = accessToken.RefreshToken,
                },
                UserProfile = userProfile
            };
        }

#nullable enable
        UserProfile? GetUserProfile(LineSDKLoginResult arg1)
#nullable disable
        {
            var profile = arg1.GetUserProfile();
            if (profile is null)
                return null;

            return new UserProfile
            {
                UserId = profile.UserID,
                DisplayName = profile.DisplayName,
                PictureURL = profile.PictureURL,
                StatusMessage = profile.StatusMessage
            };
        }

        /// <summary>
        /// AccessToken取得処理
        /// LineSDKObjC.LineSDKAccessTokenにでIDTokenRawが取得できないため
        /// LineSDKAccessToken.Jsonから取得する
        /// https://github.com/line/line-sdk-ios-swift/blob/master/LineSDK/LineSDK/Login/Model/AccessToken.swift
        /// https://github.com/line/line-sdk-ios-swift/blob/master/LineSDK/LineSDKObjC/Login/Model/LineSDKAccessToken.swift
        /// </summary>
        AccessToken GetAccessToken(LineSDKAccessToken accessToken)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(accessToken.Json);
        }
    }
}
