using System;
using System.Threading.Tasks;

namespace Entap.Basic
{
    public interface IPageNavigator
    {
        Task SetHomePageAsync();

        Task SetSplashPageAsync();
        Task SetGuidePageAsync();
        Task SetTermsTopPageAsync();
        Task PushModalTermsPageAsync();
        Task SetLoginPortalPage();

        
    }
}
