# Entap.Basic.Firebase.Auth
[Plugin.FirebaseAuth](https://github.com/f-miyu/Plugin.FirebaseAuth)をラップしたライブラリです。

## 前提
Entap.Basicでは様々な認証方法をカバーするため、[Firebase Authentication](https://firebase.google.com/docs/auth?hl=ja)を使用します。

## 導入方法
共通プロジェクトに、Entap.Basic.Firebase.Authを追加してください。

### iOS
・AppDelegateのFinishedLaunchingで初期化をしてください。
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    global::Xamarin.Forms.Forms.Init();
    Entap.Basic.iOS.Platform.Init();
    /// Entap.Basic.Firebase.Auth
    Entap.Basic.Firebase.Auth.iOS.Platform.Init();

	/// 省略

    return base.FinishedLaunching(app, options);
}
```

### Android
・MainActivityのOnCreateで初期化をしてください。
```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    TabLayoutResource = Resource.Layout.Tabbar;
    ToolbarResource = Resource.Layout.Toolbar;

    base.OnCreate(savedInstanceState);

    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    Entap.Basic.Android.Platform.Init(this);
    /// Entap.Basic.Firebase.Auth
    Entap.Basic.Firebase.Auth.Android.Platform.Init(this, savedInstanceState);

	/// 省略

     LoadApplication(new App());
}
```


## 実装例
### Password認証
```csharp
var passwordAuthService = new PasswordAuthService();
passwordAuthService.SignUpAsync("email", "password");
passwordAuthService.SignInAsync("email", "password");
```

### Twitter認証
```csharp
var twitterAuthService = new TwitterAuthService();
twitterAuthService.SignInAsync();
```

## 関連ライブラリ
* Entap.Basic.Firebase.Auth.Apple(準備中) 
* Entap.Basic.Firebase.Auth.Facebook(準備中) 
* Entap.Basic.Firebase.Auth.Google(準備中) 
* Entap.Basic.Firebase.Auth.Line(準備中) 