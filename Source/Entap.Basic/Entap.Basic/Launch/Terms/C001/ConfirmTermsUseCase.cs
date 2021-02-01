﻿using System;
using Entap.Basic.Core;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class ConfirmTermsUseCase : IConfirmTermsUseCase
    {
        public ConfirmTermsUseCase()
        {
        }

        public void ConfirmTerms()
        {
            PushTermsPage();
        }

        public void ConfirmPrivacyPolicy()
        {
            PushTermsPage();
        }

        public void ChangeChecked(bool isChecked)
        {
            System.Diagnostics.Debug.WriteLine($"ConfirmTermsUseCase.ChangeChecked() : {isChecked}");
        }

        public void AcceptTerms()
        {
        }

        void PushTermsPage()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await PageManager.Navigation.PushModalAsync<TermsPage>(new TermsPageViewModel(new TermsUseCase()));
            });
        }
    }
}
