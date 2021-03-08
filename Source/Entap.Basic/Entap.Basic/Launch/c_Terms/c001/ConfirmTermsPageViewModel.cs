using System;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class ConfirmTermsPageViewModel : PageViewModelBase
    {
        IConfirmTermsUseCase _confirmTermsUseCase;
        public ConfirmTermsPageViewModel(IConfirmTermsUseCase confirmTermsUseCase)
        {
            _confirmTermsUseCase = confirmTermsUseCase;

            AcceptCommand = new Command(() =>
            {
                _confirmTermsUseCase.AcceptTerms();
            }, () => IsChecked);
        }

        public Command ConfirmTermsCommand => new Command(() =>
        {
            _confirmTermsUseCase.ConfirmTerms();
        });

        public Command ConfirmPrivacyPolicyCommand => new Command(() =>
        {
            _confirmTermsUseCase.ConfirmPrivacyPolicy();
        });

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (SetProperty(ref _isChecked, value))
                {
                    _confirmTermsUseCase.ChangeChecked(_isChecked);
                    AcceptCommand?.ChangeCanExecute();
                }
            }
        }
        bool _isChecked;

        public Command AcceptCommand { get; set; }
    }
}
