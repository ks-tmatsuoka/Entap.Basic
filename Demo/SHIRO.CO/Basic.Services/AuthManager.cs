using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Firebase.Auth;
using Plugin.FirebaseAuth;
using Xamarin.Forms;

namespace SHIRO.CO
{
    public class AuthManager : IAuthManager, IAuthErrorCallback, IPasswordAuthErrorCallback
    {
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
                            await OnNetWorkError();
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

        public virtual async Task HandleSignUpErrorAsync(Exception exception)
        {
            switch (exception)
            {
                case FirebaseAuthException ex:
                    // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                    switch (ex.ErrorType)
                    {
                        case ErrorType.NetWork:
                            await OnNetWorkError();
                            break;
                        // email-already-in-use
                        case ErrorType.UserCollision:
                            await OnSignUpError("このメールアドレスは既に登録されています。");
                            break;
                        // invalid-email
                        case ErrorType.Email:
                            await OnSignUpError("メールアドレスを正しく入力してください。");
                            break;
                        // weak-password
                        case ErrorType.WeakPassword:
                            await OnSignUpError("半角英数字8文字以上で登録してください。");
                            break;
                        default:
                            await OnSignUpError("エラーが発生したため登録できませんでした。再度入力してください。");
                            break;
                    }
                    break;
                default:
                    await OnSignUpError();
                    break;
            }

            async Task OnSignUpError(string errorMessage = "エラーが発生したため登録できませんでした。再度入力してください。")
            {
                await OnError("登録できません", errorMessage);
            }
        }

        public virtual async Task HandleSendPasswordResetEmailErrorAsync(Exception exception)
        {
            throw new NotImplementedException();
        }

        public virtual async Task HandleVerifyPasswordResetCodeErrorAsync(Exception exception)
        {
            throw new NotImplementedException();
        }

        public virtual async Task HandleConfirmPasswordResetErrorAsync(Exception exception)
        {
            throw new NotImplementedException();
        }

        Task OnNetWorkError()
            => OnError("通信エラー", "インターネットに接続されていません。通信環境をご確認ください。");

        Task OnError(string errorTitle, string errorMessage)
        {
            return Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert(errorTitle, errorMessage, "OK");
            });
        }
    }
}
