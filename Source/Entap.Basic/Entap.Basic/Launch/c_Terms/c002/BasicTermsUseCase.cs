using System;
using Entap.Basic.Core;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class BasicTermsUseCase : ITermsUseCase
    {
        public BasicTermsUseCase()
        {
        }

        public virtual void Close()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await PageManager.Navigation.PopModalAsync();
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
