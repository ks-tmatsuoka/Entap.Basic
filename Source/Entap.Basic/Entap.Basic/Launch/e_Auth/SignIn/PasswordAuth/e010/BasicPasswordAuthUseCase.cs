using System;
namespace Entap.Basic.Launch.Auth
{
    public class BasicPasswordAuthUseCase : IPasswordAuthUseCase
    {
        public BasicPasswordAuthUseCase()
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
