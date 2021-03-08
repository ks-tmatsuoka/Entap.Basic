using System;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.LoginPortal
{
    public interface ILoginPortalUseCase : IPageLifeCycle
    {
        /// <summary>
        /// 認証スキップ
        /// </summary>
        void SkipAuth();
    }
}
