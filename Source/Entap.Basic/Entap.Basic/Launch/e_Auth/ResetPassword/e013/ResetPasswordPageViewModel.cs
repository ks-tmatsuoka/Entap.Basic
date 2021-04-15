using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class ResetPasswordPageViewModel : PageViewModelBase
    {
        readonly IResetPasswordPageUseCase _useCase;

        public ResetPasswordPageViewModel(string actionCode = null)
        {
            _useCase = BasicStartup.GetUseCase<IResetPasswordPageUseCase>();
            SetPageLifeCycle(_useCase);

            ResetCommand = new Command(
                () => _useCase.ResetPassword(actionCode, Password),
                () =>
                    !string.IsNullOrEmpty(Password) &&
                    !IsPasswordError);
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    OnPropertyChanged(nameof(PasswordErrorMessage));
                    OnPropertyChanged(nameof(IsPasswordError));
                    ResetCommand?.ChangeCanExecute();
                }
            }
        }
        string _password;

        public string PasswordErrorMessage => _useCase.ValidatePassword(Password);

        public bool IsPasswordError => !string.IsNullOrEmpty(PasswordErrorMessage);

        public Command ResetCommand { get; set; }
    }
}
