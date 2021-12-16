# Entap.Basic.Auth.Apple  
[Sign in with Apple](https://developer.apple.com/jp/sign-in-with-apple/get-started/)を行うライブラリです。  
※Android、iOS13未満、カスタムスタイルはサポート対象外です。

Apple提供のボタンを使用する場合は、別途[Entap.Basic.Auth.Apple.Forms](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Apple.Forms/Entap.Basic.Auth.Apple.Forms.NuGet)を使用してください。


## 事前準備
「Sign In with Apple」機能を有効にしてください  
[https://help.apple.com/developer-account/?lang=ja#/devde676e696](https://help.apple.com/developer-account/?lang=ja#/devde676e696)

## 導入方法
共通プロジェクトに「Entap.Basic.Auth.Apple」をインストールしてください。  

## 使用方法
### 初期化処理
* 初期化処理を実行してください、引数には必要に応じてスコープを指定してください。
```csharp
if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
    Entap.Basic.Auth.Apple.AppleSignInService.Init(
        Entap.Basic.Auth.Apple.Abstract.AuthorizationScope.Email,
        Entap.Basic.Auth.Apple.Abstract.AuthorizationScope.FullName
    );
```

### 認証処理
* SignInAsyncのレスポンス([AppleIdCredential](../Entap.Basic.Auth.Apple.Abstract.AppleIdCredential))について  
必要に応じてKeyChain等に保存するようにしてください。
  * 同一アカウントでの2回目の認証以降、Appleの使用上FullNameが取得できません。  
  本来はEmailも取得できませんが、ライブラリ内で他の情報から補完しています。
  * 後述の[AppleId使用停止時処理](#appleid使用停止時処理)を使用時、AppleIdCredential.UserIdが必要になります。  
```csharp
try
{
    // ToDo : 必要なスコープを指定してください
    var service = new Entap.Basic.Auth.Apple.AppleSignInService();
);
    var result = await service.SignInAsync();
}
catch (OperationCanceledException)
{
    // ToDo : キャンセル時処理
}
catch (Exception ex)
{
    // ToDO : エラー処理
}
```

### AppleId使用停止時処理  
AppleId使用停止時にログアウト等が必要な場合には、  
AppDelegate等でRegisterCredentialRevokedActionAsyncを実行してください。  
[userId]は認証時に取得したAppleIdCredential.UserIdを指定してください。  
この処理時の実行時点または実行後にAppleIdの使用が停止されるとactionに指定した処理を実行します。
```csharp
AppleSignInService.RegisterCredentialRevokedActionAsync([userId], () =>
{
    // ToDo : ログアウト等
}).ContinueWith((arg) =>
{
    if (arg.IsFaulted)
    {
        // ToDo :  エラー処理
    }
}).ConfigureAwait(false);
```
