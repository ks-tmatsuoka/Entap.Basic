# Entap.Basic.BackgroundGeolocation
アプリがバックグラウンドで起動中及び非起動中に位置情報の取得を行うインターフェースを提供します。
取得処理はプロジェクト毎に実装し、 Dependency Injectionします。  

## 概要
### Android
Androidでは位置情報の取得方法が複数あるので、要件に応じた実装方法を検討してください。  
[参考：Androidアプリでバックグラウンド状態で位置情報が取得できるのか調査した](https://blog.ch3cooh.jp/entry/android/get_location_information_in_background)
>Androidには位置情報の取得方法がたくさんあるので、アプリの要件によって位置情報の取得方法を使い分けたい。
「現在地から最寄りの店舗を探すような1回だけ位置情報を取得できればよいアプリ」や「位置情報を共有するアプリ」では、普通に位置情報を取得すればよい。
「経路情報を取得するマラソンアプリ」では、リアルタイムでの位置情報取得が必要になるためフォアグラウンド・サービスを利用する。
「店舗への入店/退店を監視するアプリ」や「帰宅したらエアコンをつけるアプリ」では、ジオフェンス機能を利用する。バックグラウンド位置情報利用の審査が必要になる。
「家族の位置情報共有アプリ」では、WorkManagerを利用する。これはバックグラウンド位置情報利用の審査が必要になる。

このライブラリでは、ForegroundServiceを利用した処理をサポートします。  
※省電力モード時は、位置情報の取得頻度が著しく低下します。

### iOS
iOSでの位置情報は以下の処理でモニタリングできます。
■CLLocationManager.startUpdatingLocation
https://developer.apple.com/documentation/corelocation/cllocationmanager/1423750-startupdatinglocation
・Backgroundでも位置情報の取得が可能
・消費電力は大きいが、細かく位置情報の取得が可能

■CLLocationManager.startMonitoringSignificantLocationChanges
https://developer.apple.com/documentation/corelocation/cllocationmanager/1423531-startmonitoringsignificantlocati
・ユーザーの場所の大幅な変更が検出された場合にのみ更新イベントを生成
・アプリが非起動でも位置情報の取得が可能

このライブラリではアプリのライフサイクルに応じたメソッドを用意し、バックグラウンドおよび非起動中に位置情報を取得できるよう制御します。  


## 導入方法
* 各プロジェクトでIGeolocationServiceを継承した位置情報取得処理を実装します。
* アプリケーション起動時にDIコンテナに登録します。
```csharp
void ConfigureServices()
{
    Entap.Basic.Core.BasicStartup.Current.ConfigureGeolocationService<GeolocationService>();
}
```

* GeolocationListenerを実行し、位置情報の取得を行います。
```csharp
// 開始時
await GeolocationListener.Current.StartListeningAsync();
// 停止時
await GeolocationListener.Current.StopListeningAsync();
```                
### Android
* フォアグラウンドサービスを利用するた、通知チャネルを作成やIntentの起動を行うGeolocationNotificationProvider実装します。  
[サンプル参照](/Sample.Android/Modules/GeolocationNotificationProvider.cs)  
* GeolocationNotificationProviderを登録する  
```csharp
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
{
	protected override void OnCreate(Bundle savedInstanceState)
	{
	    base.OnCreate(savedInstanceState);

	    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
	    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
	    Entap.Basic.Forms.Android.Platform.Init(this);
	    ConfigureServices();
	    LoadApplication(new App());
	}
	void ConfigureServices()
	{
	    Entap.Basic.Core.BasicStartup.Current.ConfigureGeolocationNotificationProvider<GeolocationNotificationProvider>();
	}
}
```

### iOS
* アプリ非起動時に位置情報を取得する必要がある場合、AppDelegateでGeolocationListenerのライフサイクル毎の処理を実行します。
```csharp
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        global::Xamarin.Forms.Forms.Init();
        Entap.Basic.Forms.iOS.Platform.Init();

        LoadApplication(new App());
        GeolocationListener.Current.OnDidFinishLaunchingWithOptions(app, options);

        return base.FinishedLaunching(app, options);
    }

    public override void WillEnterForeground(UIApplication uiApplication)
    {
        base.WillEnterForeground(uiApplication);
        GeolocationListener.Current.OnWillEnterForeground(uiApplication);
    }

    public override void DidEnterBackground(UIApplication uiApplication)
    {
        GeolocationListener.Current.OnDidEnterBackground(uiApplication);
        base.DidEnterBackground(uiApplication);
    }

    public override void WillTerminate(UIApplication uiApplication)
    {
        GeolocationListener.Current.OnDidEnterBackground(uiApplication);
        base.WillTerminate(uiApplication);
    }
}
```
## Sample処理の実装
位置情報の取得は[Plugin.Geolocator(https://github.com/jamesmontemagno/GeolocatorPlugin)]を利用しています。  

### iOS
[Plugin.Geolocator(https://github.com/jamesmontemagno/GeolocatorPlugin)]では、StartListeningAsyncの引数のlistenerSettingsにListenForSignificantChangesを指定することで  
StartMonitoringSignificantLocationChanges/StartUpdatingLocationの切り分けができるようになっています。
サンプルでは、アプリ起動中//バックグラウンドで起動中はStartUpdatingLocationで詳細な位置情報を取得し  
アプリ非起動中はStartMonitoringSignificantLocationChangesで大幅な位置情報の変更を取得しています。
