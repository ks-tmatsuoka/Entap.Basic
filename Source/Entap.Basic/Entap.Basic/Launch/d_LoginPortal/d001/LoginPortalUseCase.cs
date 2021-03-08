using System;
namespace Entap.Basic.Launch.LoginPortal
{
    public class LoginPortalUseCase : ILoginPortalUseCase
    {
        public LoginPortalUseCase()
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
