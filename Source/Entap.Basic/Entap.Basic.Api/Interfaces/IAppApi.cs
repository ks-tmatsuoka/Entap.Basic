using System;
using System.Threading;
using System.Threading.Tasks;

namespace Entap.Basic.Api
{
    /// <summary>
    /// アプリのAPI
    /// https://github.com/entap/laravel-template/blob/main/reference/App.v1.yaml
    /// </summary>
    public interface IAppApi
    {
        /// <summary>
        /// 通知先を登録する
        /// ユーザーの通知先の中でtokenが重複する場合は上書きする。
        /// </summary>
        /// [Post("/notification/devices")]
        Task PostNotificationDevices(RequestPostNotificationDevices request, CancellationToken token = default);

        /// <summary>
        /// 通知先を削除する。
        /// トークンに対応する通知先がなければ何もしない。
        /// </summary>
        /// [Delete("/notification/devices/{deviceToken}")]
        Task DeleteNotificationDevices(RequestDeleteNotificationDevices request, CancellationToken token = default);
    }
}
