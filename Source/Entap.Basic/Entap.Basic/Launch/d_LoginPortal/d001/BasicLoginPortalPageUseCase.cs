using System;
using Entap.Basic.Core;

namespace Entap.Basic.Launch.LoginPortal
{
    public class BasicLoginPortalPageUseCase : ILoginPortalPageUseCase
    {
        public BasicLoginPortalPageUseCase()
        {
        }

        public virtual void SignUp()
        {
            ProcessManager.Current.Invoke(async() =>
            {
                await BasicStartup.PageNavigator.PushSignUpPageAsync();
            });
        }

        #region SignIn
        public virtual void SkipAuth()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                // ToDo 匿名ログイン
                await BasicStartup.PageNavigator.SetHomePageAsync();
            });
        }

        public virtual void SignInWithEmailAndPassword()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.PageNavigator.PushPasswordSignInPageAsync();
            });
        }

        public virtual void SignInWithEmailLink()
        {
            throw new NotImplementedException();
        }

        public virtual void SignInWithFacebook()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.AuthManager.FacebookAuthService.SignInAsync();
                await BasicStartup.PageNavigator.SetHomePageAsync();
            });
        }

        public virtual void SignInWithTwitter()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.AuthManager.TwitterAuthService.SignInAsync();
                await BasicStartup.PageNavigator.SetHomePageAsync();
            });
        }

        public virtual void SignInWithGoogle()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.AuthManager.GoogleAuthService.SignInAsync();
                await BasicStartup.PageNavigator.SetHomePageAsync();
            });
        }

        public virtual void SignInWithMicrosoft()
        {
            throw new NotImplementedException();
        }

        public virtual void SignInWithApple()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.AuthManager.AppleAuthService.SignInAsync();
                await BasicStartup.PageNavigator.SetHomePageAsync();
            });
        }

        public virtual void SignInWithLine()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.AuthManager.LineAuthService.SignInAsync();
                await BasicStartup.PageNavigator.SetHomePageAsync();
            });
        }

        public virtual void SignInWithSMS()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
