using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Entap.Basic.BackgroundGeolocation
{
    public partial class GeolocationListener
    {
        public async Task PlatformStartListeningAsync()
        {
            var canStart = await _geolocationService.CanStartListeningAsync();
            if (!canStart)
                return;
            await _geolocationService.StartListeningAsync();
        }

        public Task PlatformStopListeningAsync()
            => _geolocationService.StopListeningAsync();

        public async Task PlatformStartBackgroundListeningAsync()
        {
            var canStart = await _geolocationService.CanStartBackgroundListeningAsync();
            if (!canStart)
                return;

            await _geolocationService.StartBackgroundListeningAsync();
        }

        public Task PlatformStopBackgroundListeningAsync()
            => _geolocationService.StopBackgroundListeningAsync();

        public void OnDidFinishLaunchingWithOptions(UIApplication uiApplication, NSDictionary options)
        {
            if (options is null) return;
            if (!options.ContainsKey(UIApplication.LaunchOptionsLocationKey)) return;

            Task.Run(async () =>
            {
                await PlatformStopBackgroundListeningAsync();
                await PlatformStartBackgroundListeningAsync();
            });
        }

        public void OnWillEnterForeground(UIApplication uiApplication)
        {
            Task.Run(async () =>
            {
                await PlatformStopBackgroundListeningAsync();
            });
        }

        public void OnDidEnterBackground(UIApplication uiApplication)
        {
            Task.Run(async () =>
            {
                await PlatformStartBackgroundListeningAsync();
            });
        }
    }
}
