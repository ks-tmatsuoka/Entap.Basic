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
