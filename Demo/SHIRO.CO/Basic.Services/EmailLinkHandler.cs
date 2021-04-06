using System;
using System.Threading.Tasks;
using Entap.Basic;
using Entap.Basic.Firebase.Auth.EmailLink;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Auth;

namespace SHIRO.CO
{
    public class EmailLinkHandler : Entap.Basic.Firebase.Auth.EmailLink.EmailLinkHandler
    {
        new public static EmailLinkHandler Current => LazyInitializer.Value;
        static readonly Lazy<EmailLinkHandler> LazyInitializer = new Lazy<EmailLinkHandler>(() => new EmailLinkHandler());

        public EmailLinkHandler()
        {
        }

        public override async void OnResetPassword(EmailActionParameter parameter)
        {
            base.OnResetPassword(parameter);
            var email = await BasicStartup.AuthManager.PasswordAuthService.VerifyPasswordResetCodeAsync(parameter.OobCode);
            if (string.IsNullOrEmpty(email)) return;

            await PageManager.Navigation.PushNavigationModalAsync<ResetPasswordPage>(new ResetPasswordPageViewModel(parameter.OobCode));
        }
    }
}
