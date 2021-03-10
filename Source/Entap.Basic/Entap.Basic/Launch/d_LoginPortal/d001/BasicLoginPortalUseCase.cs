using System;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic.Launch.LoginPortal
{
    public class BasicLoginPortalUseCase : ILoginPortalUseCase
    {
        public BasicLoginPortalUseCase()
        {
        }

        public virtual void SignUp()
        {
            ProcessManager.Current.Invoke(() =>
            {
                PageManager.Navigation.PushAsync<SignUpPage>(new SignUpPageViewModel());
            });
        }

        #region SignIn
        public virtual void SkipAuth()
        {
            // ToDo 匿名ログイン
            NavigateToHomePage();
        }

        public virtual void SignInWithEmailAndPassword()
        {
            throw new NotImplementedException();
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

        void NavigateToHomePage()
        {
            var pageNavigator = Startup.ServiceProvider.GetService<IPageNavigator>();
            pageNavigator.SetHomePage();
        }
    }
}
