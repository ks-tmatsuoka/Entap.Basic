using System;
namespace Entap.Basic.Launch.LoginPortal
{
    public class BasicLoginPortalUseCase : ILoginPortalUseCase
    {
        public BasicLoginPortalUseCase()
        {
        }

        public virtual void SkipAuth()
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
