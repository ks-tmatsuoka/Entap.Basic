using System;
using Entap.Basic.Core;

namespace Entap.Basic.Launch.Terms
{
    public class BasicConfirmTermsPageUseCase : IConfirmTermsPageUseCase
    {
        public BasicConfirmTermsPageUseCase()
        {
        }

        public virtual void ConfirmTerms()
        {
            PushTermsPage();
        }

        public virtual void ConfirmPrivacyPolicy()
        {
            PushTermsPage();
        }

        public virtual void ChangeChecked(bool isChecked)
        {
            System.Diagnostics.Debug.WriteLine($"ConfirmTermsUseCase.ChangeChecked() : {isChecked}");
        }

        public virtual void AcceptTerms()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.PageNavigator.SetLoginPortalPage();
            });
        }

        void PushTermsPage()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await BasicStartup.PageNavigator.PushModalTermsPageAsync();
            });
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
