using System;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.LoginPortal
{
    public interface ILoginPortalUseCase : IPageLifeCycle
    {
        /// <summary>
        /// 認証スキップ
        /// </summary>
        void SkipAuth();

        /// <summary>
        /// 新規登録
        /// </summary>
        void SignUp();

        /// <summary>
        /// パスワード認証
        /// </summary>
        void SignInWithEmailAndPassword();

        /// <summary>
        /// メールリンク認証
        /// </summary>
        void SignInWithEmailLink();

        /// <summary>
        /// Facebook認証
        /// </summary>
        void SignInWithFacebook();

        /// <summary>
        /// Twitter認証
        /// </summary>
        void SignInWithTwitter();

        /// <summary>
        /// Google認証
        /// </summary>
        void SignInWithGoogle();

        /// <summary>
        /// Microsoft認証
        /// </summary>
        void SignInWithMicrosoft();

        /// <summary>
        /// Apple認証
        /// </summary>
        void SignInWithApple();

        /// <summary>
        /// LINE認証
        /// </summary>
        void SignInWithLine();

        /// <summary>
        /// SMS認証
        /// </summary>
        void SignInWithSMS();
    }
}
