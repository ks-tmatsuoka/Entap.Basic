# Entap.Basic.Auth.Apple.Forms  
[Sign in with Apple](https://developer.apple.com/jp/sign-in-with-apple/)で使用する[ASAuthorizationAppleIDButton](https://developer.apple.com/documentation/authenticationservices/asauthorizationappleidbutton)をXamarin.Formsで使用可能なようにラップしたライブラリです。  
※Android、iOS13未満、カスタムスタイルはサポート対象外です。

外観の指定、カスタムスタイルについては[Human Interface Guidelines](https://developer.apple.com/design/human-interface-guidelines/sign-in-with-apple/overview/buttons/)を確認してください。

## 導入方法
共通プロジェクトに「Entap.Basic.Auth.Apple.Forms」をインストールしてください。  
**共通プロジェクト**  
以下の初期化処理を実行してください。
```csharp
public App()
{
    InitializeComponent();

    ...
    Entap.Basic.Auth.Apple.Forms.Controls.Init();
    ...

}
```
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

## 使用例
名前空間は以下のいづれかを指定してください。
・xmlns:basic="http://entap.co.jp/schemas/basic"  
・xmlns:basic="clr-namespace:Entap.Basic.Auth.Apple.Forms;assembly=Entap.Basic.Auth.Apple.Forms"

```xml
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:Sample"
    xmlns:basic="http://entap.co.jp/schemas/basic"
    x:Class="Sample.AppleSignInPage"
    x:DataType="local:AppleSignInPageViewModel"
>
    <StackLayout
        Padding="16"
    >
        <!-- 引数省略時は、ButtonType:SigniIn, ButtonStyle:Black-->
        <basic:AppleSignInButton/>

        <!-- ButtonType, ButtonStyle指定時-->
        <basic:AppleSignInButton>
            <x:Arguments>
                <basic:ButtonType>Continue</basic:ButtonType>
                <basic:ButtonStyle>Black</basic:ButtonStyle>
            </x:Arguments>

    </StackLayout>
</ContentPage>
```

## サンプル画像
<img src="Images/AppleSignInButton.png" width="300" />