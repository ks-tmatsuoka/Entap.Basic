using System;
using System.Threading.Tasks;
using Entap.Basic;
using Entap.Basic.Launch.Splash;

namespace SHIRO.CO
{
    public class SplashPageUseCase : BasicSplashPageUseCase
    {
        public SplashPageUseCase()
        {
        }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();
            if (BasicStartup.PageNavigator is not PageNavigator navigator) return;
            {
                if (navigator.PendingNavigation is not null)
                {
                    await navigator.PendingNavigation.Invoke();
                    navigator.PendingNavigation = null;
                }
            }
        }
    }
}
