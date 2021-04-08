using System;
using System.Threading.Tasks;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Firebase.Auth;
using Entap.Basic.Firebase.Auth.Facebook;
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
        public IPasswordAuthService PasswordAuthService => _passwordAuthService ??= new PasswordAuthService(this);
        IPasswordAuthService _passwordAuthService;
        #endregion

        #region TwitterAuth
        public bool IsTwitterAuthSupported => TwitterAuthService is not null;
        public ISnsAuthService TwitterAuthService => _twitterAuthService ??= new TwitterAuthService(this);
        ISnsAuthService _twitterAuthService;
        #endregion

        #region FacebookAuth
        public bool IsFacebookAuthSupported => FacebookAuthService is not null;
        public ISnsAuthService FacebookAuthService => _facebookAuthService ??= new FacebookAuthService();
        ISnsAuthService _facebookAuthService;
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
                case OperationCanceledException:
                    // ユーザによるキャンセルはスキップ
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

        public virtual async Task HandleSignOutErrorAsync(Exception exception)
        {
            switch (exception)
            {
                case FirebaseAuthException ex:
                    switch (ex.ErrorType)
                    {
                        case ErrorType.NetWork:
                            await OnNetWorkError();
                            break;
                        default:
                            await OnSignOutError();
                            break;
                    }
                    break;
                default:
                    await OnSignOutError();
                    break;
            }

            async Task OnSignOutError(string errorMessage = "エラーが発生したためログアウトできませんでした。")
            {
                await OnError("ログアウトできません", errorMessage);
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
            switch (exception)
            {
                // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                case FirebaseAuthException ex:
                    switch (ex.ErrorType)
                    {
                        case ErrorType.NetWork:
                            await OnNetWorkError();
                            break;
                        case ErrorType.InvalidUser:
                            await OnSendPasswordResetEmailError("メールアドレスを正しく入力してください。");
                            break;
                        case ErrorType.InvalidCredentials:
                            await OnSendPasswordResetEmailError("このメールアドレスは登録されていません。");
                            break;
                        default:
                            await OnSendPasswordResetEmailError();
                            break;
                    }
                    break;
                default:
                    await OnSendPasswordResetEmailError();
                    break;
            }
            // ToDo : 文言設定依頼中
            async Task OnSendPasswordResetEmailError(string errorMessage = "メールの送信に失敗しました")
            {
                await OnError("ログインできません", errorMessage);
            }
        }

        public virtual async Task HandleVerifyPasswordResetCodeErrorAsync(Exception exception)
        {
            // ToDo : 文言設定依頼中
            switch (exception)
            {
                // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                case FirebaseAuthException ex:
                    switch (ex.ErrorType)
                    {
                        case ErrorType.NetWork:
                            await OnNetWorkError();
                            break;
                        // user-not-found, user-disabled
                        case ErrorType.InvalidUser when ex.ErrorCode == Entap.Basic.Firebase.Auth.ErrorCode.UserDisabled:
                            // ToDo Plugin.FireBaseAuth 4.1.0ではこの条件のエラーは発生しない（ConfirmPasswordResetAsyncで発生）
                            await OnVerifyPasswordResetCodeError("該当のアカウントは無効化されています");
                            break;
                        // expired-action-code, invalid-action-code(user-not-found)
                        case ErrorType.ActionCode:
                        default:
                            await OnVerifyPasswordResetCodeError("再度リセットメールを送信してください。");
                            break;
                    }
                    break;
                default:
                    await　OnVerifyPasswordResetCodeError();
                    break;
            }
            async Task OnVerifyPasswordResetCodeError(string errorMessage = "認証に失敗しました。。")
            {
                await OnError("認証できません", errorMessage);
            }
        }

        public virtual async Task HandleConfirmPasswordResetErrorAsync(Exception exception)
        {
            // ToDo : 文言設定依頼中
            switch (exception)
            {
                // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                case FirebaseAuthException ex:
                    switch (ex.ErrorType)
                    {
                        case ErrorType.NetWork:
                            await OnNetWorkError();
                            break;
                        // user-disabled
                        case ErrorType.InvalidUser when ex.ErrorCode == ErrorCode.UserDisabled:
                            await OnConfirmPasswordResetError("該当のアカウントは無効化されています");
                            break;
                        // weak-password
                        case ErrorType.WeakPassword:
                            await OnConfirmPasswordResetError("半角英数字8文字以上で登録してください。");
                            break;
                        // expired-action-code, invalid-action-code(user-not-found)
                        case ErrorType.ActionCode:
                        default:
                            await OnConfirmPasswordResetError();
                            break;
                    }
                    break;
                default:
                    await OnConfirmPasswordResetError();
                    break;
            }
            async Task OnConfirmPasswordResetError(string errorMessage = "パスワード再設定用のメールを再送し、再度お試しください。")
            {
                await OnError("パスワード変更エラー", errorMessage);
            }
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
