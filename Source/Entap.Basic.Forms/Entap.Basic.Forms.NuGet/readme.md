# Entap.Basic.Forms  
## 導入方法
* 各プロジェクトにEntap.Basic.Formsを追加してください。  

### iOS
・AppDelegateのFinishedLaunchingで初期化をしてください。
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    global::Xamarin.Forms.Forms.Init();

    Entap.Basic.Forms.iOS.Platform.Init();

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
    Entap.Basic.Forms.Android.Platform.Init(this);

	/// 省略

     LoadApplication(new App());
}
```

## 機能  
### PageManager  

### DisplaySizeManaer  
[ScreenSize]
画面のサイズ(ナビゲーションバーやステータスバーの高さを含む)を取得できます。

[PageSize]
ページのサイズ(ナビゲーションバーやステータスバーの高さは含まない)を取得できます。

[iOSのPageSizeおよびTopNavigationHeightについて]
iOSでは各ページのコードビハインドやViewModelのコンストラクタ内で使用するとNavigationControllerが取得できないタイミングの関係で、正確な値を取得できません。その際はiOSDisplaySizeChangedを使用して値を取得してください。

[ナビゲーションバーがない時のSizeの計算方法]
・iOSでナビゲーションバーない場合について
TopNavigationHeightとPageSizeとiOSDisplaySizeRecivedのNavigationBarHeight、PageHeightは、HasNavigationBar="False"でナビゲーション使ってない場合でもナビゲーションバーがある際のサイズを返すので、ナビゲーションバーのないページでページのサイズ取得したい場合PageSizeのHeightにTopNavigationHeightを足した数値が正確なPageSizeとなります。
MainPageにNavigationPageを使ってない場合はPageSizeのHeightにTopNavigationHeightを足す必要はないです。

・Androidでナビゲーションバーない場合について
TopNavigationHeightとPageSizeとiOSDisplaySizeRecivedのNavigationBarHeight、PageHeightは、HasNavigationBar="False"およびSetMainPageでナビゲーション使ってない場合でもナビゲーションバーがある際のサイズを返すので、ナビゲーションバーのないページでページのサイズ取得したい場合PageSizeのHeightにTopNavigationHeightを足した数値が正確なPageSizeとなります。

iOS Androidともにナビゲーションバーがない場合のPageの高さはScreenSize - StatusBarHeight で取得できます。
