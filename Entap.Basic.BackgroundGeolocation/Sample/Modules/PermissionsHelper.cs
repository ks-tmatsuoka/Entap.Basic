using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace LRMS
{
    public class PermissionsHelper
    {
        public static async Task<PermissionStatus> RequestLocationAlwaysPermissionIfNeededAsync()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                // LocationWhenInUseの許可を要求後に、LocationAlwaysを要求することで「常に許可」を選択可能とする。
                var locationWhenInUseStatus = await RequestPermissionIfNeededAsync<Permissions.LocationWhenInUse>();
                if (locationWhenInUseStatus != PermissionStatus.Granted)
                    return locationWhenInUseStatus;

                await RequestPermissionIfNeededAsync<Permissions.LocationAlways>();
                // LocationAlwaysで使用中のみを選択した場合、PermissionStatusはDeniedになるため
                // LocationWhenInUseのステータスを返す。
                return locationWhenInUseStatus;
            }
            else
                return await RequestPermissionIfNeededAsync<Permissions.LocationAlways>();
        }

        /// <summary>
        /// パーミッションの権限が付与されていない場合、パーミッションを要求する。
        /// </summary>
        /// <typeparam name="TPermission"></typeparam>
        /// <returns></returns>
        async static Task<PermissionStatus> RequestPermissionIfNeededAsync<TPermission>() where TPermission : BasePermission, new()
        {
            var result = await Device.InvokeOnMainThreadAsync(async () => await CheckStatusAsync<TPermission>());
            if (result == PermissionStatus.Granted)
                return result;
            else
                return await Device.InvokeOnMainThreadAsync(async () => await RequestAsync<TPermission>());
        }
    }
}
