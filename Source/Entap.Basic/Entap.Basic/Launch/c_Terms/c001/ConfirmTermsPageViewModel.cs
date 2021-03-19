using System;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class ConfirmTermsPageViewModel : PageViewModelBase
    {
        readonly IConfirmTermsPageUseCase _useCase;
        public ConfirmTermsPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<IConfirmTermsPageUseCase>();
            SetPageLifeCycle(_useCase);

            AcceptCommand = new Command(() =>
            {
                _useCase.AcceptTerms();
            }, () => IsChecked);
        }

        public Command ConfirmTermsCommand => new Command(() =>
        {
            _useCase.ConfirmTerms();
        });

        public Command ConfirmPrivacyPolicyCommand => new Command(() =>
        {
            _useCase.ConfirmPrivacyPolicy();
        });

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (SetProperty(ref _isChecked, value))
                {
                    _useCase.ChangeChecked(_isChecked);
                    AcceptCommand?.ChangeCanExecute();
                }
            }
        }
        bool _isChecked;

        public Command AcceptCommand { get; set; }
    }
}
