using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Entap.Basic.BackgroundGeolocation
{
    public partial class GeolocationListener
    {
        public Task PlatformStartListeningAsync() => throw new PlatformNotSupportedException();

        public Task PlatformStopListeningAsync() => throw new PlatformNotSupportedException();

        public Task PlatformStartBackgroundListeningAsync() => throw new PlatformNotSupportedException();

        public Task PlatformStopBackgroundListeningAsync() => throw new PlatformNotSupportedException();
    }
}
