using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Guide;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic.Settings
{
    public class BasicSettingsPageUseCase : ISettingsPageUseCase
    {
        readonly IPageNavigator _pageNavigator;
        readonly IPasswordAuthService _passwordAuthService;
        public BasicSettingsPageUseCase()
        {
            _pageNavigator = Startup.ServiceProvider.GetService<IPageNavigator>();
            _passwordAuthService = Startup.ServiceProvider.GetService<IPasswordAuthService>();
        }


        public void SignOut()
        {
            ProcessManager.Current.Invoke(async() =>
            {
                _passwordAuthService.SignOut();
                await _pageNavigator.SetGuidePageAsync();
            });
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
