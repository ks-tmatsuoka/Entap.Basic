using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Core;

namespace Entap.Basic.Settings
{
    public class BasicSettingsPageUseCase : ISettingsPageUseCase
    {
        public BasicSettingsPageUseCase()
        {
        }


        public void SignOut()
        {
            ProcessManager.Current.Invoke(async() =>
            {
                await BasicStartup.AuthManager.SignOutAsync();
                await BasicStartup.PageNavigator.SetGuidePageAsync();
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
