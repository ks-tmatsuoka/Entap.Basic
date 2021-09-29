# Entap.Basic.Auth.Apple.Forms  
[Sign in with Apple](https://developer.apple.com/jp/sign-in-with-apple/)で使用する[ASAuthorizationAppleIDButton](https://developer.apple.com/documentation/authenticationservices/asauthorizationappleidbutton)をXamarin.Formsで使用可能なようにラップしたライブラリです。  
※Android、iOS13未満、カスタムスタイルはサポート対象外です。

## 導入方法
共通プロジェクトに「Entap.Basic.Auth.Apple.Forms」をインストールしてください。  
**iOS**  
・AppDelegateのFinishedLaunchingで初期化をしてください。  
※iOS13未満をターゲットにする場合は、iOS13以上の場合のみ初期化するようにしてください。
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
	...
    if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
        Entap.Basic.Auth.Apple.Forms.iOS.Platform.Init();
	...

    LoadApplication(new App());
    return base.FinishedLaunching(app, options);
}
```