using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Splash
{
    public interface ISplashUseCase : IPageLifeCycle
    {
        /// <summary>
        /// ロード処理
        /// </summary>
        /// <returns>タスク</returns>
        Task LoadAsync();
    }
}
