using System;
using System.Threading.Tasks;

namespace Entap.Basic.Launch.Splash
{
    public class BasicSplashPageUseCase : ISplashPageUseCase
    {
        public BasicSplashPageUseCase()
        {
        }

        public virtual async Task LoadAsync()
        {
            await Task.Delay(3000);
            await BasicStartup.PageNavigator.SetGuidePageAsync();
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
