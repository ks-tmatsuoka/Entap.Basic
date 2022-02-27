using System;
using Entap.Basic.Core.Android;

namespace Entap.Basic.BackgroundGeolocation
{
    /// <summary>
    /// 位置情報取得をForegroundServiceで実行するためのNotification情報を提供
    /// </summary>
    public interface IGeolocationNotificationProvider : INotificationProvider
    {
    }
}