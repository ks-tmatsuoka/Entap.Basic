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
            throw new NotImplementedException();
        }

        public virtual void SignInWithTwitter()
        {
            throw new NotImplementedException();
        }

        public virtual void SignInWithGoogle()
        {
            throw new NotImplementedException();
        }

        public virtual void SignInWithMicrosoft()
        {
            throw new NotImplementedException();
        }

        public virtual void SignInWithApple()
        {
            throw new NotImplementedException();
        }

        public virtual void SignInWithLine()
        {
            throw new NotImplementedException();
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
