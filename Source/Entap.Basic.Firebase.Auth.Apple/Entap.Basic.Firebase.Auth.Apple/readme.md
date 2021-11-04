# Entap.Basic.Firebase.Auth.Apple  
[Firebase Authentication](https://firebase.google.com/docs/auth?hl=ja)を使用し、
[Sign in with Apple](https://developer.apple.com/jp/sign-in-with-apple/get-started/)を行うライブラリです。  

Firebase Authenticationには、[Plugin.FirebaseAuth](https://github.com/f-miyu/Plugin.FirebaseAuth)を  
Sign in with Appleには[Entap.Basic.Auth.Apple](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Apple/Entap.Basic.Auth.Apple.NuGet)を使用します。


## 準備
[Entap.Basic.Auth.Apple](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Line/Entap.Basic.Auth.Line.NuGet)の[事前準備](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Apple/Entap.Basic.Auth.Apple.NuGet#%E4%BA%8B%E5%89%8D%E6%BA%96%E5%82%99)、[導入方法](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Apple/Entap.Basic.Auth.Apple.NuGet#%E5%B0%8E%E5%85%A5%E6%96%B9%E6%B3%95)を確認してください。


## 使用方法
```csharp
var authService = new AppleAuthService([errorCallback], [scopes]);
await authService.SignInAsync();
```