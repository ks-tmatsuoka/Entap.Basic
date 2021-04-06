using System;
using System.Threading.Tasks;
using Entap.Basic;
using Entap.Basic.Firebase.Auth.EmailLink;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Auth;
using Entap.Basic.Launch.Splash;

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
            var email = await BasicStartup.AuthService.VerifyPasswordResetCodeAsync(parameter.OobCode);
            if (string.IsNullOrEmpty(email)) return;

            if (PageManager.Navigation.GetCurrentPage().GetType() == typeof(SplashPage))
            {
                if (BasicStartup.PageNavigator is not PageNavigator navigator) return;
                navigator.SetPendingNavigation(async () =>
                {
                    await PageManager.Navigation.PushNavigationModalAsync<ResetPasswordPage>(new ResetPasswordPageViewModel(parameter.OobCode));
                });
                return;
            }
            await PageManager.Navigation.PushNavigationModalAsync<ResetPasswordPage>(new ResetPasswordPageViewModel(parameter.OobCode));
        }
    }
}
