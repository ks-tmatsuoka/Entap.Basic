using System;
namespace Entap.Basic.Launch.Terms
{
    public class ConfirmTermsUseCase : IConfirmTermsUseCase
    {
        public ConfirmTermsUseCase()
        {
        }

        public void ConfirmTerms()
        {
        }

        public void ConfirmPrivacyPolicy()
        {
        }

        public void ChangeChecked(bool isChecked)
        {
            System.Diagnostics.Debug.WriteLine($"ConfirmTermsUseCase.ChangeChecked() : {isChecked}");
        }

        public void AcceptTerms()
        {
        }
    }
}
