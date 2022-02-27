using System;
using System.Threading.Tasks;
using Android.Content;
using Entap.Basic.Core.Android;

namespace Entap.Basic.BackgroundGeolocation
{
    public partial class GeolocationListener
    {
        public const string ActionStopService = "Entap.Basic.BackgroundGeolocation.Action.STOP_FOREGROUND_SERVICE";

        Context Context => Platform.Activity;

        public Task PlatformStartListeningAsync()
            => PlatformStartBackgroundListeningAsync();

        public Task PlatformStopListeningAsync()
            => PlatformStopBackgroundListeningAsync();

        public async Task PlatformStartBackgroundListeningAsync()
        {
            try
            {
                await PlatformStopBackgroundListeningAsync();
            }
            catch { }
            var canStart = await _geolocationService.CanStartListeningAsync();
            if (!canStart)
                return;

            Context.StartForegroundServiceCompat<GeolocationForegroundService>();
        }

        public Task PlatformStopBackgroundListeningAsync()
        {
            var result = Context.StopService<GeolocationForegroundService>();
            return (result) ?
                Task.CompletedTask :
                Task.FromException(new InvalidOperationException($"{typeof(GeolocationListener).FullName}.{nameof(PlatformStopBackgroundListeningAsync)}"));
        }
    }
}
