using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic.Launch.Splash
{
    public class BasicSplashPageUseCase : ISplashPageUseCase
    {
        readonly IPageNavigator _pageNavigator;
        public BasicSplashPageUseCase()
        {
            _pageNavigator = Startup.ServiceProvider.GetService<IPageNavigator>();
        }

        public virtual async Task LoadAsync()
        {
            await Task.Delay(3000);
            await _pageNavigator.SetGuidePageAsync();
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
