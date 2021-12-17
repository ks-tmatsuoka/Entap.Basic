# Entap.Basic.Auth.Line
[LINEログイン](https://developers.line.biz/ja/reference/line-login/)(v2.1)のWeb認証で行います。  
Web認証には[Xamarin.Essentials.Web Authenticator](https://docs.microsoft.com/ja-jp/xamarin/essentials/web-authenticator)を使用しています。  
※iOSについては、
https://github.com/xamarin/Essentials/issues/1242
https://github.com/xamarin/Essentials/issues/1519

## 事前準備
[LINE Developers](https://developers.line.biz/ja/)のアカウントを作成し、Provider・Channelを作成し  
LINE Login settingsを登録してください。

## 導入方法

**iOS**  
 ・[アプリをチャネルにリンクする](https://developers.line.biz/ja/docs/ios-sdk/swift/setting-up-project/#linking-app-to-channel)  
　※ユニバーサルリンクの[アプリとサーバーを関連づけ](https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#ul-s1)については 、[Firebase Dynamic Links](https://firebase.google.com/docs/dynamic-links?hl=ja)の使用も可能です(シミュレータはサポート対象外)。    
・[Info.plistファイルを設定する](https://developers.line.biz/ja/docs/ios-sdk/swift/setting-up-project/#config-infoplist-file)  
・「Entap.Basic.Auth.Line.iOS」を追加し、AppDelegateのContinueUserActivityにLINE認証後処理を実行してくださいください。
```csharp
public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
{
    if (Entap.Basic.Auth.Line.iOS.WebAuthenticationService.ContinueUserActivity(application, userActivity, completionHandler))
        return true;

    return base.ContinueUserActivity(application, userActivity, completionHandler);
}
```
**Android**  
コールバックURIを処理するためにインテントフィルターの設定が必要です。  
以下のようなクラスを定義してください。  
```csharp
[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
    Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
    DataScheme = "[http/https]", DataHost = "[host]", DataPath = "[path]")]
public class LineWebAuthenticationCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
{
}
```

プロジェクトのターゲットAndroidバージョンをAndroid 11 (R API 30)に設定する場合、
AndoridManifestに以下の記載が必要になります。  
　※[Android 11 でのパッケージへのアクセス](https://developer.android.com/about/versions/11/privacy/package-visibility)参照
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="jp.co.entap.shiro.co">
	...

	<queries>
	    <intent>
	        <action android:name="android.support.customtabs.action.CustomTabsService" />
	    </intent>
	</queries>

	...
</manifest>
```

## 使用方法
```csharp
LineAuthService.Init(new LineAuthParameter([clientId], [clientSecret], [scope], [redirectUri]);
...

var authService = new LineAuthService();
var token = await authService.LoginAsync();
```
