# Entap.Basic.Auth.Google  
Google サインインを行うライブラリです。  
* Android	:https://developers.google.com/identity/sign-in/android/start?hl=ja  
* iOS		:https://developers.google.com/identity/sign-in/ios?hl=ja

## 事前準備
Firebaseプロジェクトを作成し、Googleサインインを有効にしてください。  
* Android	:https://firebase.google.com/docs/auth/android/google-signin?hl=ja#before_you_begin  
* iOS		:https://firebase.google.com/docs/auth/ios/google-signin?hl=ja#before_you_begin  


## 導入方法

### Android  
* Xamarin.Firebase.Authをインストールしてください。
* AndroidManifest.xmlで「Internet」のパーミッションを指定してください。
```xml
<uses-permission android:name="android.permission.INTERNET"/>
```

* MainActivityのOnCreateで初期化処理を追加してください。
```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
	base.OnCreate(savedInstanceState);

	global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

	// Google SignIn
	Entap.Basic.Auth.Google.Android.Platform.Init(this, [authClinetId], [requestCode]);

	LoadApplication(new App());
}
```


### iOS  
* Xamarin.Firebase.iOS.Authをインストールしてください。  
* AppDelegateのFinishedLaunchingで初期化処理を追加してください。
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
	global::Xamarin.Forms.Forms.Init();

	// Google SignIn
	Entap.Basic.Auth.Google.iOS.Platform.Init(
		Firebase.Core.Options.DefaultInstance.ClientId,
		Xamarin.Essentials.Platform.GetCurrentUIViewController);

	LoadApplication(new App());

	return base.FinishedLaunching(app, options);
}
```
* AppDelegateのOpenUrlでURLのハンドリング処理を追加してください。
```csharp
public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
{
    // Google SignIn
    return Google.SignIn.SignIn.SharedInstance.HandleUrl(url);
}
```


## 使用方法
```csharp
var authService = new GoogleAuthService();
var authentication = await authService.AuthAsync();
```