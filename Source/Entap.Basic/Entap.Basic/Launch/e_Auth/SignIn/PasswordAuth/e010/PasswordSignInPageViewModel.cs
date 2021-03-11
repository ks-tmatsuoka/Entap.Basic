using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic.Launch.Auth
{
    public class PasswordSignInPageViewModel : PageViewModelBase
    {
        readonly IPasswordSignInUseCase _passwordSignInUseCase;
        public PasswordSignInPageViewModel()
        {
            _passwordSignInUseCase = Startup.ServiceProvider.GetService<IPasswordSignInUseCase>();
            SetPageLifeCycle(_passwordSignInUseCase);

            SignInCommand = new Command(
                () => _passwordSignInUseCase.SignIn(MailAddress, Password),
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

        public string MailAddressErrorMessage => _passwordSignInUseCase.ValidateMailAddress(MailAddress);

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

        public string PasswordErrorMessage => _passwordSignInUseCase.ValidatePassword(Password);

        public bool IsPasswordError => !string.IsNullOrEmpty(PasswordErrorMessage);

        public Command SignInCommand { get; set; }

        public Command ResetPasswordCommand => new Command(() => _passwordSignInUseCase.ResetPassword());
    }
}
