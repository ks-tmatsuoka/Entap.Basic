using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class BasicResetPasswordPageUseCase : IResetPasswordPageUseCase
    {
        public BasicResetPasswordPageUseCase()
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
