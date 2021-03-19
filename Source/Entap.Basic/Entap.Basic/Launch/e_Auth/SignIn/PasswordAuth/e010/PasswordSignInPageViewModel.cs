using System;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class PasswordSignInPageViewModel : PageViewModelBase
    {
        readonly IPasswordSignInPageUseCase _useCase;
        public PasswordSignInPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<IPasswordSignInPageUseCase>();
            SetPageLifeCycle(_useCase);

            SignInCommand = new Command(
                () => _useCase.SignIn(MailAddress, Password),
                () =>
                    !string.IsNullOrEmpty(MailAddress) &&
                    !IsMailAddressError &&
                    !string.IsNullOrEmpty(Password));
        }

        public string MailAddress
        {
            get => _mailAddress;
            set
            {
                if (SetProperty(ref _mailAddress, value))
                {
                    OnPropertyChanged(nameof(MailAddressErrorMessage));
                    OnPropertyChanged(nameof(IsMailAddressError));
                    SignInCommand?.ChangeCanExecute();
                }
            }
        }
        string _mailAddress;

        public string MailAddressErrorMessage => _useCase.ValidateMailAddress(MailAddress);

        public bool IsMailAddressError => !string.IsNullOrEmpty(MailAddressErrorMessage);

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    OnPropertyChanged(nameof(PasswordErrorMessage));
                    OnPropertyChanged(nameof(IsPasswordError));
                    SignInCommand?.ChangeCanExecute();
                }
            }
        }
        string _password;

        public string PasswordErrorMessage => _useCase.ValidatePassword(Password);

        public bool IsPasswordError => !string.IsNullOrEmpty(PasswordErrorMessage);

        public Command SignInCommand { get; set; }

        public Command ResetPasswordCommand => new Command(() => _useCase.ResetPassword());
    }
}
