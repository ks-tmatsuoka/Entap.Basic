using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Entap.Basic.Core;

namespace Entap.Basic.BackgroundGeolocation
{
    [Service(ForegroundServiceType = ForegroundService.TypeLocation | ForegroundService.TypeDataSync)]
    public class GeolocationForegroundService : Service
    {
        IGeolocationService _geolocationService;
        IGeolocationNotificationProvider _geolocationNotificationProvider;
        public override IBinder OnBind(Intent intent) => null;

        public override void OnCreate()
        {
            base.OnCreate();
            _geolocationService = BasicStartup.Current.GetService<IGeolocationService>();
            if (_geolocationService is null)
                throw new InvalidOperationException($"{nameof(IGeolocationService)} is not registered");

            _geolocationNotificationProvider = BasicStartup.Current.GetService<IGeolocationNotificationProvider>();
            if (_geolocationNotificationProvider is null)
                throw new InvalidOperationException($"{nameof(IGeolocationNotificationProvider)} is not registered");
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent?.Action == GeolocationListener.ActionStopService)
            {
                StopForeground(true);
                StopSelf();
            }
            else
            {
                _geolocationNotificationProvider.CreateNotificationChannel();

                var notificationId = _geolocationNotificationProvider.NotificationId;
                var notification = _geolocationNotificationProvider.Notification;

                StartForeground(notificationId, notification);

                StartListeningAsync()
                    .ContinueWith((arg) =>
                    {
                        if (!arg.IsCompletedSuccessfully)
                            StopSelf();
                    })
                    .ConfigureAwait(false);
            }
            return StartCommandResult.Sticky;
        }

        public override bool StopService(Intent name)
        {
            StopListeningAsync().ConfigureAwait(false);
            return base.StopService(name);
        }

        public override void OnDestroy()
        {
            StopListeningAsync().ConfigureAwait(false);
            StopSelf();
            base.OnDestroy();
        }

        async Task StartListeningAsync()
        {
            await _geolocationService.StartListeningAsync();
        }

        async Task StopListeningAsync()
        {
            await _geolocationService.StopListeningAsync();
        }
    }
}
