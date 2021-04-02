using System;
using System.Threading.Tasks;

namespace Entap.Basic
{
    public interface IPageNavigator
    {
        Task SetStartUpPageAsync();
        Task SetHomePageAsync();

        Task SetSplashPageAsync();
        Task SetGuidePageAsync();
        Task SetTermsTopPageAsync();
        Task PushModalTermsPageAsync();

        Task SetLoginPortalPage();
        Task PushPasswordSignInPageAsync();
        Task PushSendPasswordResetEmailPageAsync();
        Task PushResetPasswordPageAsync();

        Task PushSignUpPageAsync();
    }
}
