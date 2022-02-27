using System;
using System.Threading.Tasks;
using Entap.Basic.Core;

namespace Entap.Basic.BackgroundGeolocation
{
    public partial class GeolocationListener
    {
        static readonly Lazy<GeolocationListener> _instance = new Lazy<GeolocationListener>(() => new GeolocationListener());
        public static GeolocationListener Current => _instance.Value;

        IGeolocationService _geolocationService;
        public GeolocationListener()
        {
            _geolocationService = BasicStartup.Current.GetService<IGeolocationService>();
        }

        public Task StartListeningAsync() => PlatformStartListeningAsync();

        public Task StopListeningAsync() => PlatformStopListeningAsync();

        public Task StartBackgroundListeningAsync() => PlatformStartBackgroundListeningAsync();

        public Task StopBackgroundListeningAsync() => PlatformStopBackgroundListeningAsync();
    }
}
