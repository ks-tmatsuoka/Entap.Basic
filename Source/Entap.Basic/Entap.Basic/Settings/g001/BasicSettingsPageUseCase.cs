using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Forms;

namespace Entap.Basic.Settings
{
    public class BasicSettingsPageUseCase : ISettingsPageUseCase
    {
        public BasicSettingsPageUseCase()
        {
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
