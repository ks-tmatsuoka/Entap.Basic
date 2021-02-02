using System;
using System.Threading.Tasks;

namespace Entap.Basic.Launch.Splash
{
    public interface ISplashUseCase
    {
        /// <summary>
        /// ロード処理
        /// </summary>
        /// <returns>タスク</returns>
        Task LoadAsync();
    }
}
