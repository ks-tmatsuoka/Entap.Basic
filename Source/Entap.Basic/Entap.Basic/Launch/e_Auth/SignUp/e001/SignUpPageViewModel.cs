using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class SignUpPageViewModel : PageViewModelBase
    {
        readonly ISignUpUseCase _signUpUseCase;
        public SignUpPageViewModel(ISignUpUseCase signUpUseCase)
        {
            _signUpUseCase = signUpUseCase;
            SignUpCommand = new Command(
                () => _signUpUseCase.SignUp(MailAddress, Password),
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

        public string MailAddressErrorMessage => _signUpUseCase.ValidateMailAddress(MailAddress);

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

        public string PasswordErrorMessage => _signUpUseCase.ValidatePassword(Password);

        public bool IsPasswordError => !string.IsNullOrEmpty(PasswordErrorMessage);

        public Command SignUpCommand { get; set; }
    }
}
