using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Auth;
using Entap.Basic.Launch.Guide;
using Entap.Basic.Launch.LoginPortal;
using Entap.Basic.Launch.Splash;
using Entap.Basic.Launch.Terms;

namespace Entap.Basic
{
    public class BasicPageNavigator : IPageNavigator
    {
        public virtual Task SetStartUpPageAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task SetHomePageAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task SetSplashPageAsync()
        {
            return PageManager.Navigation.SetMainPage<SplashPage>(new SplashPageViewModel());
        }

        public virtual Task SetGuidePageAsync()
        {
            // ToDo
            var contents = new List<GuideContent>()
            {
                new GuideContent { Title = "title 1", Description = "description 1", Next = "つぎへ" },
                new GuideContent { Title = "title 2", Description = "description 2", Next = "つぎへ" },
                new GuideContent { Title = "title 3", Description = "description 3", Next = "はじめる" }
            };
            return PageManager.Navigation.SetMainPage<GuidePage>(new GuidePageViewModel(contents));
        }

        public virtual Task SetTermsTopPageAsync()
        {
            return PageManager.Navigation.SetMainPage<ConfirmTermsPage>(new ConfirmTermsPageViewModel());
        }

        public virtual Task PushModalTermsPageAsync()
        {
            return PageManager.Navigation.SetMainPage<ConfirmTermsPage>(new ConfirmTermsPageViewModel());
        }

        public virtual Task SetLoginPortalPage()
        {
            return PageManager.Navigation.SetNavigationMainPage<LoginPortalPage>(new LoginPortalPageViewModel());
        }

        public virtual Task PushPasswordSignInPageAsync()
        {
            return PageManager.Navigation.PushAsync<PasswordSignInPage>(new PasswordSignInPageViewModel());
        }

        public virtual Task PushSendPasswordResetEmailPageAsync()
        {
            return PageManager.Navigation.PushAsync<SendPasswordResetEmailPage>(new SendPasswordResetEmailPageViewModel());
        }

        public virtual Task PushResetPasswordPageAsync()
        {
            return PageManager.Navigation.PushAsync<ResetPasswordPage>(new ResetPasswordPageViewModel());
        }

        public Task PushSignUpPageAsync()
        {
            throw new NotImplementedException();
        }
    }
}
