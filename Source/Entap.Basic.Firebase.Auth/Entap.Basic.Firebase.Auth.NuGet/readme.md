# Entap.Basic.Firebase.Auth
[Plugin.FirebaseAuth](https://github.com/f-miyu/Plugin.FirebaseAuth)をラップしたライブラリです。

## 前提
Entap.Basicでは様々な認証方法をカバーするため、[Firebase Authentication](https://firebase.google.com/docs/auth?hl=ja)を使用します。

## 導入方法
・各プロジェクトにPlugin.FirebaseAuth, Entap.Basic.Api, Entap.Basic.Firebase.Authを追加してください。  

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
このライブラリでは、FirebaseにサインインしFirebaseのIdTokenを使用しサーバ認証を行いサーバのアクセストークンを取得・保存します。  
### サーバとの通信処理
[Entap.Basic.Api.IAuthApi](https://github.com/entap/Entap.Basic/blob/main/Source/Entap.Basic/Entap.Basic.Api/Interfaces/IAuthApi.cs)の実装クラスを作成し、BasicFirebaseAuthStartUpに登録してください。  
```csharp
BasicFirebaseAuthStartUp.ConfigureAuthApi<[IAuthApi]>();
```

### アクセストークンの保存処理
[Basic.Firebase.AuthUserDataRepository](https://github.com/entap/Entap.Basic/blob/main/Source/Entap.Basic.Firebase.Auth/Entap.Basic.Firebase.Auth/Modules/UserDataRepository.cs)を利用する場合は、[IAccessTokenPreferencesService](https://github.com/entap/Entap.Basic/blob/main/Source/Entap.Basic.Firebase.Auth/Entap.Basic.Firebase.Auth/Interfaces/IAccessTokenPreferencesService.cs)の実装クラスを定義しBasicFirebaseAuthStartUpに登録してください。
([Xamarin.Essentials.SecureStorage](https://docs.microsoft.com/ja-jp/xamarin/essentials/secure-storage?tabs=android)推奨)
```csharp
BasicFirebaseAuthStartUp.ConfigureAccessTokenPreferencesService<[IAccessTokenPreferencesService]>();
```
[UserDataRepository](https://github.com/entap/Entap.Basic/blob/main/Source/Entap.Basic.Firebase.Auth/Entap.Basic.Firebase.Auth/Modules/UserDataRepository.cs)を継承して利用する場合は、BasicFirebaseAuthStartUpに登録してください。 
```csharp
BasicFirebaseAuthStartUp.ConfigureUserDataRepository<[IUserDataRepository]>();
```

### 認証サービスの登録
使用する認証サービスをBasicFirebaseAuthStartUpに登録してください。
```csharp
BasicFirebaseAuthStartUp.ConfigurePasswordAuthService<PasswordAuthService>();
BasicFirebaseAuthStartUp.ConfigureTwitterAuthService<TwitterAuthService>();
// 以下別途ライブラリ追加要
BasicFirebaseAuthStartUp.ConfigureFacebookAuthService<FacebookAuthService>();
BasicFirebaseAuthStartUp.ConfigureLineAuthService<LineAuthService>();
BasicFirebaseAuthStartUp.ConfigureGoogleAuthService<GoogleAuthService>();
BasicFirebaseAuthStartUp.ConfigureAppleAuthService<AppleAuthService>();
```

### 認証エラーコールバックの登録
認証エラー時やユーザによるキャンセル時にFirebaseAuthExceptionやOperationCanceledExceptionが発生します。
認証サービスでエラー処理を共通化させたい場合は、BasicFirebaseAuthStartUpに登録をして下さい。
```csharp
BasicFirebaseAuthStartUp.ConfigureAuthErrorCallback<[IAuthErrorCallback]>();
BasicFirebaseAuthStartUp.ConfigurePasswordAuthErrorCallback<[IPasswordAuthErrorCallback]>();
```

### 実行
```csharp
var authManager = new FirebaseAuthManager();
// Password認証
authManager.PasswordAuthService.SignUpAsync("email", "password");
authManager.PasswordAuthService.SignInAsync("email", "password");

// Twitter認証
authManager.TwitterAuthService.SignInAsync();
```


## 関連ライブラリ
* [Entap.Basic.Firebase.Auth.Apple](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Firebase.Auth.Apple/Entap.Basic.Firebase.Auth.Apple)
* [Entap.Basic.Firebase.Auth.Facebook(準備中)](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Firebase.Auth.Facebook/Entap.Basic.Firebase.Auth.Facebook)
* [Entap.Basic.Firebase.Auth.Google(準備中)](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Firebase.Auth.Google/Entap.Basic.Firebase.Auth.Google)
* [Entap.Basic.Firebase.Auth.Line](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Firebase.Auth.Line/Entap.Basic.Firebase.Auth.Line)