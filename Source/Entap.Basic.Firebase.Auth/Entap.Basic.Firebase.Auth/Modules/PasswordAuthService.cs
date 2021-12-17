using System;
using System.Threading.Tasks;
using Entap.Basic.Auth.Abstractions;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class PasswordAuthService : IPasswordAuthService
    {
        readonly IPasswordAuthErrorCallback _errorCallback;
        public PasswordAuthService(IPasswordAuthErrorCallback errorCallback = null)
        {
            _errorCallback = errorCallback;
        }


        /// <summary>
        /// 新しいユーザーアカウントの作成とサインインを行い、ユーザトークンを返す
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <returns>トークン</returns>
        /// https://firebase.google.com/docs/reference/js/firebase.auth.Auth?hl=ja#createuserwithemailandpassword
#nullable enable
        public async Task<string?> SignUpAsync(string email, string password)
#nullable disable
        {
            try
            {
                var result = await CrossFirebaseAuth.Current.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return await result?.User?.GetIdTokenAsync(false);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleSignUpErrorAsync(ex);
                throw;
            }
        }

        /// <summary>
        /// メールアドレスとパスワードでサインインを行い、ユーザトークンを返す
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
#nullable enable
        public async Task<string?> SignInAsync(string email, string password)
#nullable disable
        {
            try
            {
                var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(email, password);
                return await result?.User?.GetIdTokenAsync(false);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleSignInErrorAsync(ex);
                throw;
            }
        }

        /// <summary>
        /// サインアウト
        /// </summary>
        public void SignOut()
        {
            CrossFirebaseAuth.Current.Instance.SignOut();
        }

        /// <summary>
        /// パスワードリセットメール送信
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// https://firebase.google.com/docs/reference/js/firebase.auth.Auth?hl=ja#sendpasswordresetemail
        public Task SendPasswordResetEmailAsync(string email)
            => SendPasswordResetEmailAsync(email, null);

        /// <summary>
        /// パスワードリセットメール送信
        /// </summary>
        /// <param name="BasicSendPasswordResetEmailParameter">SendPasswordResetEmailParameter</param>
        /// https://firebase.google.com/docs/reference/js/firebase.auth.Auth?hl=ja#sendpasswordresetemail
        public async Task SendPasswordResetEmailAsync(string email, ActionCodeSettings settings)
        {
            try
            { 
                await CrossFirebaseAuth.Current.Instance.SendPasswordResetEmailAsync(email, settings);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleSendPasswordResetEmailErrorAsync(ex);
                throw;
            }
        }

        /// <summary>
        /// パスワードリセット時のアクションコード検証
        /// </summary>
        /// <param name="actionCode">パスワードリセットメールのURLに保有するアクションコード</param>
        /// <returns>メールアドレス</returns>
        public async Task<string> VerifyPasswordResetCodeAsync(string actionCode)
        {
            try
            {
                return await CrossFirebaseAuth.Current.Instance.VerifyPasswordResetCodeAsync(actionCode);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleVerifyPasswordResetCodeErrorAsync(ex);
                throw;
            }
        }

        /// <summary>
        /// パスワードリセット処理
        /// </summary>
        /// <param name="actionCode">パスワードリセットメールのURLに保有するアクションコード</param>
        /// <param name="newPassword">パスワード</param>
        public async Task ConfirmPasswordResetAsync(string actionCode, string newPassword)
        {
            try
            {
                await CrossFirebaseAuth.Current.Instance.ConfirmPasswordResetAsync(actionCode, newPassword);
            }
            catch (Exception ex)
            {
                await _errorCallback?.HandleConfirmPasswordResetErrorAsync(ex);
                throw;
            }
        }
    }
}
