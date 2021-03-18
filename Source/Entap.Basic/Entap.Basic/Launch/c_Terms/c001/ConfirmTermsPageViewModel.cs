using System;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class ConfirmTermsPageViewModel : PageViewModelBase
    {
        readonly IConfirmTermsPageUseCase _useCase;
        public ConfirmTermsPageViewModel()
        {
            _useCase = Startup.ServiceProvider.GetService<IConfirmTermsPageUseCase>();
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
