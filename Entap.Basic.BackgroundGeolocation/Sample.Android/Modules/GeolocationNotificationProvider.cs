using System;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using Entap.Basic.BackgroundGeolocation;

namespace LRMS.Droid
{
    public class GeolocationNotificationProvider : IGeolocationNotificationProvider
    {
        public GeolocationNotificationProvider()
        {
        }

        Context Context => Xamarin.Essentials.Platform.AppContext;
        string ChannelId => NotificationDefinitions.Geolocation.ChannelId;

        public int NotificationId => NotificationDefinitions.Geolocation.NotificationId;

        public Notification Notification => new NotificationCompat.Builder(Context, ChannelId)
                .SetContentTitle(Xamarin.Essentials.AppInfo.Name)
                .SetContentText("位置情報取得中")
                .SetSmallIcon(Resource.Mipmap.icon)
                .SetAutoCancel(false)
                .SetOngoing(true)
                .SetContentIntent(GetPendingIntent())
                .AddAction(GetStopAction())
                .Build();

        public void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                return;

            var notificationChannel = new NotificationChannel
            (
                ChannelId,
                NotificationDefinitions.Geolocation.ChannelName,
                NotificationImportance.Default
            );

            var notificationManager = (NotificationManager)Context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);
        }

        PendingIntent GetPendingIntent()
        {
            var intent = new Intent(Context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);
            return PendingIntent.GetActivity(Context, 1, intent, PendingIntentFlags.UpdateCurrent);
        }

        NotificationCompat.Action GetStopAction()
        {
            var stopServiceIntent = new Intent(Context, typeof(GeolocationForegroundService));
            stopServiceIntent.SetAction(GeolocationListener.ActionStopService);
            var stopServicePendingIntent = PendingIntent.GetService(Context, 0, stopServiceIntent, 0);

            var builder = new NotificationCompat.Action.Builder(null, "停止", stopServicePendingIntent);
            return builder.Build();

        }
    }
}
