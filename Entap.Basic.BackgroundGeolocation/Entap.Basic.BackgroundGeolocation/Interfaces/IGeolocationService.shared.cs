using System.Threading.Tasks;

namespace Entap.Basic.BackgroundGeolocation
{
    /// <summary>
    /// 位置情報取得処理
    /// </summary>
    public interface IGeolocationService
    {
        /// <summary>
        /// 位置情報の取得が可能か
        /// </summary>
        /// <returns><c>true</c>:可, <c>false</c>:不可</returns>
        Task<bool> CanStartListeningAsync();

        /// <summary>
        /// 位置情報の取得を開始する
        /// </summary>
        Task StartListeningAsync();

        /// <summary>
        /// 位置情報の取得を停止する
        /// </summary>
        Task StopListeningAsync();


        /// <summary>
        /// バックグラウンドでの位置情報の取得が可能か
        /// </summary>
        /// <returns><c>true</c>:可, <c>false</c>:不可</returns>
        Task<bool> CanStartBackgroundListeningAsync();

        /// <summary>
        /// バックグラウンドでの位置情報の取得を開始する
        /// </summary>
        Task StartBackgroundListeningAsync();

        /// <summary>
        /// バックグラウンドでの位置情報の取得を停止する
        /// </summary>
        Task StopBackgroundListeningAsync();
    }
}