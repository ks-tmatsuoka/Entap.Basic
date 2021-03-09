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
        readonly IConfirmTermsUseCase _confirmTermsUseCase;
        public ConfirmTermsPageViewModel()
        {
            _confirmTermsUseCase = Startup.ServiceProvider.GetService<IConfirmTermsUseCase>() ?? new BasicConfirmTermsUseCase();
            SetPageLifeCycle(_confirmTermsUseCase);

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
