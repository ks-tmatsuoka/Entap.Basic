﻿using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Firebase.Auth;
using Plugin.FirebaseAuth;
using Xamarin.Forms;

namespace SHIRO.CO
{
    public class AuthManager : IAuthManager, IAuthErrorCallback
    {
        struct ErrorMessage
        {
            public const string NetWorkError = "インターネットに接続されていません。通信環境をご確認ください。";
        }
        public AuthManager()
        {
        }

        #region PasswordAuth
        public bool IsPasswordAuthSupported => PasswordAuthService is not null;
        public IPasswordAuthService PasswordAuthService => _passwordAuthService ??= new PasswordAuthService();
        IPasswordAuthService _passwordAuthService;
        #endregion

        #region TwitterAuth
        public bool IsTwitterAuthSupported => TwitterAuthService is not null;
        public ISnsAuthService TwitterAuthService => _twitterAuthService ??= new TwitterAuthService(this);
        ISnsAuthService _twitterAuthService;
        #endregion

        public virtual async Task HandleSignInErrorAsync(Exception exception)
        {
            switch (exception)
            {
                case FirebaseAuthException ex:
                    switch (ex.ErrorType)
                    {
                        case ErrorType.Web when ex.ErrorCode == ErrorCode.WebContextCancelled:
                            // ユーザによるキャンセルはスキップ
                            break;
                        case ErrorType.NetWork:
                            await OnSignInError(ErrorMessage.NetWorkError);
                            break;
                        // user-not-found, user-disabled
                        case ErrorType.InvalidUser:
                        // invalid-email, wrong-password
                        case ErrorType.InvalidCredentials:
                            if (ex.ErrorCode == Entap.Basic.Firebase.Auth.ErrorCode.UserDisabled)
                                await OnSignInError("このアカウントは利用規約に違反しているため停止となりました。ご質問等がある場合はHPからお問い合わせください。");
                            else
                                await OnSignInError("メールアドレスまたはパスワードが違います");
                            break;
                        default:
                            await OnSignInError();
                            break;
                    }
                    break;
                default:
                    await OnSignInError();
                    break;
            }

            async Task OnSignInError(string errorMessage = "エラーが発生したためログインできませんでした。")
            {
                await OnError("ログインできません", errorMessage);
            }
        }

        Task OnError(string errorTitle, string errorMessage)
        {
            return Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert(errorTitle, errorMessage, "OK");
            });
        }
    }
}
