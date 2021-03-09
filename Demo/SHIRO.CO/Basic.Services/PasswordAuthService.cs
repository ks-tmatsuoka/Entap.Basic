using System;
using System.Threading.Tasks;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Core;
using Plugin.FirebaseAuth;
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
                return await _authService.SignUpAsync(email, password);
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
                return await _authService.SignInAsync(email, password);

            }
            catch (FirebaseAuthException ex)
            {
                // https://github.com/f-miyu/Plugin.FirebaseAuth/blob/master/Plugin.FirebaseAuth/iOS/ExceptionMapper.cs
                switch (ex.ErrorType)
                {
                    case ErrorType.NetWork:
                        await OnError("ネットワークエラー");
                        break;
                    // user-not-found, FIXME:user-disabled
                    case ErrorType.InvalidUser:
                    // invalid-email, wrong-password
                    case ErrorType.InvalidCredentials:
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
            _authService.SignOut();
        }

        Task OnError(string errorMessage)
        {
            return Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert("認証エラー", errorMessage, "OK");
            });
        }

    }
}
