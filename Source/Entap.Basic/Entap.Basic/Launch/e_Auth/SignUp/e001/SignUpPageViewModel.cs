using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class SignUpPageViewModel : PageViewModelBase
    {
        readonly ISignUpPageUseCase _useCase;
        public SignUpPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<ISignUpPageUseCase>();
            SetPageLifeCycle(_useCase);

            SignUpCommand = new Command(
                () => _useCase.SignUp(MailAddress, Password),
                () =>
                    !string.IsNullOrEmpty(MailAddress)&&
                    !IsMailAddressError &&
                    !string.IsNullOrEmpty(Password) &&
                    !IsPasswordError);  
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
                    SignUpCommand?.ChangeCanExecute();
                }
            }
        }
        string _mailAddress;

        public string MailAddressErrorMessage => _useCase.ValidateMailAddress(MailAddress);

        public bool IsMailAddressError =>  !string.IsNullOrEmpty(MailAddressErrorMessage);

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    OnPropertyChanged(nameof(PasswordErrorMessage));
                    OnPropertyChanged(nameof(IsPasswordError));
                    SignUpCommand?.ChangeCanExecute();
                }
            }
        }
        string _password;

        public string PasswordErrorMessage => _useCase.ValidatePassword(Password);

        public bool IsPasswordError => !string.IsNullOrEmpty(PasswordErrorMessage);

        public Command SignUpCommand { get; set; }
    }
}
