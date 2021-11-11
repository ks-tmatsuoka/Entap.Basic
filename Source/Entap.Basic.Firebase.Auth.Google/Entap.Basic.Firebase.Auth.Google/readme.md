# Entap.Basic.Firebase.Auth.Google  
[Firebase Authentication](https://firebase.google.com/docs/auth?hl=ja)を使用し、
Google サインインを行うライブラリです。 
* Android :https://developers.google.com/identity/sign-in/android/start?hl=ja
* iOS :https://developers.google.com/identity/sign-in/ios?hl=ja 

Firebase Authenticationには、[Plugin.FirebaseAuth](https://github.com/f-miyu/Plugin.FirebaseAuth)を  
Google サインインには[Entap.Basic.Auth.Google](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Google/Entap.Basic.Auth.Google.NuGet)を使用します。


## 準備
[Entap.Basic.Auth.Google](https://github.com/entap/Entap.Basic/tree/main/Source/Entap.Basic.Auth.Google/Entap.Basic.Auth.Google.NuGet)を確認してください。


## 使用方法
```csharp
var authService = new GoogleAuthService([errorCallback]);
await authService.SignInAsync();
```