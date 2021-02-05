using System;
namespace Entap.Basic.Launch.LoginPortal
{
    public interface ILoginPortalUseCase
    {
        /// <summary>
        /// 認証スキップ
        /// </summary>
        void SkipAuth();
    }
}
