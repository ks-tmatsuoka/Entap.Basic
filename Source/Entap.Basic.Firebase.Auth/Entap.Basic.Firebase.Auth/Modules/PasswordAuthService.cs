using System;
using System.Threading.Tasks;
using Entap.Basic.Auth.Abstractions;
using Plugin.FirebaseAuth;

namespace Entap.Basic.Firebase.Auth
{
    public class PasswordAuthService : IPasswordAuthService
    {
        public PasswordAuthService()
        {
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
            var result = await CrossFirebaseAuth.Current.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            return await result?.User?.GetIdTokenAsync(false);
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
            var result = await CrossFirebaseAuth.Current.Instance.SignInWithEmailAndPasswordAsync(email, password);
            return await result?.User?.GetIdTokenAsync(false);
        }

        /// <summary>
        /// サインアウト
        /// </summary>
        public void SignOut()
        {
            CrossFirebaseAuth.Current.Instance.SignOut();
        }
    }
}
