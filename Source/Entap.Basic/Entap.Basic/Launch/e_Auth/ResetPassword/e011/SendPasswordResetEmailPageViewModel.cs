using System;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class SendPasswordResetEmailPageViewModel : PageViewModelBase
    {
        readonly ISendPasswordResetEmailPageUseCase _useCase;
        public SendPasswordResetEmailPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<ISendPasswordResetEmailPageUseCase>();
            SetPageLifeCycle(_useCase);

            SendEmailCommand = new Command(
                () => _useCase.SendPasswordResetEmail(MailAddress),
                () =>
                    !string.IsNullOrEmpty(MailAddress) &&
                    !IsMailAddressError);
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
                    SendEmailCommand?.ChangeCanExecute();
                }
            }
        }
        string _mailAddress;

        public string MailAddressErrorMessage => _useCase.ValidateMailAddress(MailAddress);

        public bool IsMailAddressError => !string.IsNullOrEmpty(MailAddressErrorMessage);

        public Command SendEmailCommand { get; set; }

    }
}
