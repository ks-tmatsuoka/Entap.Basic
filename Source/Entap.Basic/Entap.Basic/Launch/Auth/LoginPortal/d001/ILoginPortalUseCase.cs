using System;
namespace Entap.Basic.Launch.Auth
{
    public interface ILoginPortalUseCase
    {
        /// <summary>
        /// 認証スキップ
        /// </summary>
        void SkipAuth();
    }
}
