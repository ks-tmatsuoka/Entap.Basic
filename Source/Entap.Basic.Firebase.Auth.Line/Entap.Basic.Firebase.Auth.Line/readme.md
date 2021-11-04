# Entap.Basic.Firebase.Auth.Line 
[Firebase Authentication](https://firebase.google.com/docs/auth?hl=ja)を使用し、
[LINEログイン](https://developers.line.biz/ja/reference/line-login/)を行うライブラリです。

Firebase Authenticationには、[Plugin.FirebaseAuth](https://github.com/f-miyu/Plugin.FirebaseAuth)を  
LINEログインには[Entap.Basic.Auth.Line](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Line/Entap.Basic.Auth.Line.NuGet)を使用します。


## 準備
[Entap.Basic.Auth.Line](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Line/Entap.Basic.Auth.Line.NuGet)の[事前準備](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Line/Entap.Basic.Auth.Line.NuGet#%E4%BA%8B%E5%89%8D%E6%BA%96%E5%82%99)、[導入方法](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Line/Entap.Basic.Auth.Line.NuGet#%E5%B0%8E%E5%85%A5%E6%96%B9%E6%B3%95)を確認してください。


## 使用方法
```csharp
var parameter = new LineAuthParameter([clientId], [clientSecret], [scope], [redirectUri]);
var authService = new LineAuthService(parameter);
await authService.SignInAsync();
```