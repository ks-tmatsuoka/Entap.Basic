using System;
using System.Threading.Tasks;
using Entap.Basic.Api;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Core;
using Newtonsoft.Json;
using Plugin.FirebaseAuth;
using Refit;
using Xamarin.Forms;

namespace SHIRO.CO
{
    public class PasswordAuthService : IPasswordAuthService
    {
        readonly Entap.Basic.Firebase.Auth.PasswordAuthService _authService = new Entap.Basic.Firebase.Auth.PasswordAuthService();
        public PasswordAuthService()
        {
        }


        public async Task<string> SignUpAsync(string email, string password)
        {
            try
            {
                // https://firebase.google.com/docs/reference/js/firebase.auth.Auth#createuserwithemailandpassword
                var token = await _authService.SignUpAsync(email, password);
                if (token == null) return token;

                var response = await BasicApiManager.Current.CallAsync(() =>
                {
                    return BasicApiManager.Current.Api.PostAuthFirebaseUser(new FirebaseIdToken(token));
                });
                return (response?.IsSuccessStatusCode == true) ?
                    token : null;
            }
            catch (FirebaseAuthException ex)
            {
                // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                switch (ex.ErrorType)
                {
                    case ErrorType.NetWork:
                        await OnError("ネットワークエラー");
                        break;
                    // email-already-in-use
                    case ErrorType.UserCollision:
                        await OnError("メールアドレス使用済みです");
                        break;
                    // invalid-email
                    case ErrorType.Email:
                        await OnError("メールアドレスの形式エラー");
                        break;
                    // weak-password
                    case ErrorType.WeakPassword:
                        await OnError("パスワードの強度が弱いです");
                        break;
                    default:
                        // FIXME:operation-not-allowedはErrorTypeから判定不可
                        await OnError("登録に失敗しました");
                        break;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await OnError("登録に失敗しました");
                return null;
            }
        }

        public async Task<string> SignInAsync(string email, string password)
        {
            try
            {
                // https://firebase.google.com/docs/reference/js/firebase.auth.Auth#signinwithemailandpassword
                var token = await _authService.SignInAsync(email, password);
                var response = await BasicApiManager.Current.CallAsync(() =>
                {
                    return BasicApiManager.Current.Api.PostAuthFirebaseToken(new FirebaseIdToken(token));
                });
                if (response is null) return null;
                if (!response.IsSuccessStatusCode) return null;

                var accessToken = response.Content.AccessToken;
                BasicApiManager.Current.SetAuthorization(accessToken);

                return (response?.IsSuccessStatusCode == true) ?
                    response.Content.AccessToken : null;
            }
            catch (FirebaseAuthException ex)
            {
                // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                switch (ex.ErrorType)
                {
                    case ErrorType.NetWork:
                        await OnError("ネットワークエラー");
                        break;
                    // user-not-found, user-disabled
                    case ErrorType.InvalidUser:
                    // invalid-email, wrong-password
                    case ErrorType.InvalidCredentials:
                        if (ex.ErrorCode == Entap.Basic.Firebase.Auth.ErrorCode.UserDisabled)
                            await OnError("該当のアカウントは無効化されています");
                        else
                            await OnError("メールアドレスまたはパスワードが違います");
                        break;
                    default:
                        await OnError("認証に失敗しました");
                        break;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await OnError("登録に失敗しました");
                return null;
            }
        }

        public void SignOut()
        {
            try
            {
                _authService.SignOut();
            }
            catch (FirebaseAuthException ex)
            {
                // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                switch (ex.ErrorType)
                {
                    case ErrorType.NetWork:
                        OnError("ネットワークエラー");
                        break;
                    default:
                        OnError("サインアウト失敗");
                        break;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                OnError("サインアウト失敗");
            }
        }

        Task OnError(string errorMessage)
        {
            return Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert("認証エラー", errorMessage, "OK");
            });
        }

        public async Task<string> VerifyPasswordResetCodeAsync(string actionCode)
        {
            try
            {
                return await _authService.VerifyPasswordResetCodeAsync(actionCode);
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.ErrorType)
                {
                    case ErrorType.NetWork:
                        await OnError("ネットワークエラー");
                        break;
                    // expired-action-code, invalid-action-code(user-not-found)
                    case ErrorType.ActionCode:
                        await OnError("再度リセットメールを送信してください。");
                        break;
                    // user-not-found, user-disabled
                    case ErrorType.InvalidUser when ex.ErrorCode == Entap.Basic.Firebase.Auth.ErrorCode.UserDisabled:
                        // ToDo Plugin.FireBaseAuth 4.1.0ではこの条件のエラーは発生しない（ConfirmPasswordResetAsyncで発生）
                        await OnError("該当のアカウントは無効化されています");
                        break;
                    default:
                        await OnError("再度リセットメールを送信してください。");
                        break;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task ConfirmPasswordResetAsync(string actionCode, string newPassword)
        {
            try
            {
                await _authService.ConfirmPasswordResetAsync(actionCode, newPassword);
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.ErrorType)
                {
                    case ErrorType.NetWork:
                        await OnError("ネットワークエラー");
                        break;
                    // expired-action-code, invalid-action-code(user-not-found)
                    case ErrorType.ActionCode:
                        await OnError("このURLは使用済みです。再度リセットメールを送信してください。");
                        break;
                    // user-disabled
                    case ErrorType.InvalidUser when ex.ErrorCode == Entap.Basic.Firebase.Auth.ErrorCode.UserDisabled:
                        await OnError("該当のアカウントは無効化されています");
                        break;
                    default:
                        await OnError("認証エラー。再度リセットメールを送信してください。");
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
