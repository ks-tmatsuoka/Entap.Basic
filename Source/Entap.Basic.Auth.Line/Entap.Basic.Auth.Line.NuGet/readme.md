# Entap.Basic.Auth.Line [LINEログイン](https://developers.line.biz/ja/docs/line-login/)を行うライブラリです。
iOS:[LINE SDK for iOS](https://developers.line.biz/ja/docs/ios-sdk/)をバインディングした[Xamarin.LineSDK.iOS](https://github.com/entap/Xamarin.LineSDK/tree/main/Xamarin.LineSDK/Xamarin.LineSDK.iOS)を利用します。  
Android:[LINE SDK for Android](https://developers.line.biz/ja/docs/android-sdk/)をバインディングした[Xamarin.LineSDK.Android](https://github.com/entap/Xamarin.LineSDK/tree/main/Xamarin.LineSDK/Xamarin.LineSDK.Android)を利用します。

## 事前準備
[LINE Developers](https://developers.line.biz/ja/)のアカウントを作成し、Provider・Channelを作成し  
LINE Login settingsを登録してください。

## 導入方法

**iOS**  
 ・[アプリをチャネルにリンクする](htt	ps://developers.line.biz/ja/docs/ios-sdk/swift/setting-up-project/#linking-app-to-channel)  
　※ユニバーサルリンクの[アプリとサーバーを関連づけ](https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#ul-s1)については 、[Firebase Dynamic Links](https://firebase.google.com/docs/dynamic-links?hl=ja)の使用も可能です  
(シミュレータはサポート対象外)。    
・[Info.plistファイルを設定する](https://developers.line.biz/ja/docs/ios-sdk/swift/setting-up-project/#config-infoplist-file)  
・「Entap.Basic.Auth.Line」を追加してください。
・[アプリデリゲートを変更する](https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-app-delegate)
```csharp
public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
{
    return Entap.Basic.Auth.Line.LineAuthService.OpenUrl(app, url, options);
}
```

・[アプリデリゲートを変更する](https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#modify-app-delegate)（ユニバーサルリンクを利用時）
```csharp
public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
{
    if (Entap.Basic.Auth.Line.LineAuthService.ContinueUserActivity(application, userActivity, completionHandler))
        return true;

    return base.ContinueUserActivity(applicat	ion, userActivity, completionHandler);
}
```

・[シーンデリゲートを変更する](https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-scene-delegates)（マルチウィンドウをサポートする場合）
```csharp
public override void OpenUrlContexts(UIScene scene, NSSet<UIOpenUrlContext> urlContexts)
{
    Entap.Basic.Auth.Line.OpenUrlContexts.OpenUrl(scene, urlContexts);
}
```

・初期化処理
```csharp
[Register("AppDelegate")]
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        global::Xamarin.Forms.Forms.Init();troller);

		// 初期化処理（universalLinkURL：Universal Linkを利用しない場合は省略可能）
        Entap.Basic.Auth.Line.LineAuthService.Init([channelID], [universalLinkURL]);
        return base.FinishedLaunching(app, options);
    }
}
```

**Android**  
・[Androidマニフェストファイルを設定する](https://developers.line.biz/ja/docs/android-sdk/integrate-line-login/#setting-android-manifest-file])

・初期化処理
```csharp
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IOnSuccessListener
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        TabLayoutResource = Resource.Layout.Tabbar;
        ToolbarResource = Resource.Layout.Toolbar;

        base.OnCreate(savedInstanceState);

        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

        Entap.Basic.Auth.Line.LineAuthService.Init([channelID]);

        LoadApplication(new App());
    }
```

・LineLoginButton使用時
```csharp
protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
{
    base.OnActivityResult(requestCode, resultCode, data);

    if (LineAuthService.OnActivityResult(requestCode, resultCode, data))
        return;
}
```

## 使用方法
```csharp
var authService = new LineAuthService();
var result = await authService.LoginAsync([LoginScope[]]);
```

## 補足説明
### iOS
・LineLoginButtonRendererでWeakLoginDelegateを設定してもDelegateが実行されない
→既存のクリック時処理を無効化し、独自にログイン処理を実行して回避。

### Android
・LineLoginButtonRendererでLoginDelegate、LoginListenerを設定してもLoginListenerが実行されない。
→MainActivity.OnActivityResult時に、LineAuthServiceを経由しログイン結果を取得するよう変更して回避。

