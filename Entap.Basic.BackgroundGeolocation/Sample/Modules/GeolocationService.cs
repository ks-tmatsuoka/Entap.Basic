using System;
using System.Threading.Tasks;
using Entap.Basic.BackgroundGeolocation;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace LRMS
{
    public class GeolocationService : IGeolocationService
    {
        // ToDo
        const string ErrorTitle = "位置情報取得エラー";
        const string NotAvailableMessage = "このデバイスでは位置情報を取得できません。";
        const string NotEnabledMessage = "位置情報機能が「OFF」になっています。";
        const string NotGrantedMessage = "位置情報取得の権限がありません。";

        // ToDo : minumum設定
        static readonly TimeSpan MinimumTimeSpan = TimeSpan.FromMinutes(10);
        static readonly int MinimumDistanceMeters = 10;

        public GeolocationService()
        {
        }

        public async Task<bool> CanStartListeningAsync()
        {
            if (!CrossGeolocator.Current.IsGeolocationAvailable)
            {
                await DisplayErrorDialogAsync(NotAvailableMessage);
                return false;
            }

            if (!CrossGeolocator.Current.IsGeolocationEnabled)
            {
                await DisplayErrorDialogAsync(NotEnabledMessage);
                return false;
            }

            var permissionStatus = await PermissionsHelper.RequestLocationAlwaysPermissionIfNeededAsync();
            if (permissionStatus != Xamarin.Essentials.PermissionStatus.Granted)
            {
                await DisplayErrorDialogAsync(NotGrantedMessage);
                return false;
            }
            return true;
        }

        public async Task StartListeningAsync()
        { 
            await StartListeningAsync(CrossGeolocator.Current, false);
            PreferencesService.SetIsGeolocationListening(true);
        }

        public async Task StopListeningAsync()
        {
            await StopListeningAsync(CrossGeolocator.Current);
            PreferencesService.SetIsGeolocationListening(false);
        }

        public async Task<bool> CanStartBackgroundListeningAsync()
        {
            var canStart = await CanStartListeningAsync();
            if (!canStart) return canStart;
            if (Device.RuntimePlatform == Device.Android)
                return canStart;

            // フォアグラウンドで取得中のみバックグラウンドで取得する
            return PreferencesService.GetIsGeolocationListening();
        }

        IGeolocator _backgroundGeolocator;
        public Task StartBackgroundListeningAsync()
        {
            IGeolocator geolocator;
            if (Device.RuntimePlatform == Device.iOS)
            {
                geolocator = _backgroundGeolocator ?? (_backgroundGeolocator = DependencyService.Get<IPluginGeolocatorService>().GetGeolocator());
            }
            else
            {
                geolocator = CrossGeolocator.Current;
            }
            return StartListeningAsync(geolocator, true);
        }

        public async Task StopBackgroundListeningAsync()
        {
            if (_backgroundGeolocator is null) return;
            await StopListeningAsync(_backgroundGeolocator);
        }

        async Task StartListeningAsync(IGeolocator geolocator,　bool isBackground)
        {
            geolocator.PositionChanged += OnPositionChanged;
            geolocator.PositionError += OnPositionError;

            await geolocator.StartListeningAsync(MinimumTimeSpan, MinimumDistanceMeters, false, GetListenerSettings(isBackground));
        }

        async Task StopListeningAsync(IGeolocator geolocator)
        {
            geolocator.PositionError += OnPositionError;
            geolocator.PositionError -= OnPositionError;

            if (!geolocator.IsListening) return;

            await geolocator.StopListeningAsync();
        }

        ListenerSettings GetListenerSettings(bool isBackground = false)
        {
            return new ListenerSettings
            {
                AllowBackgroundUpdates = true,
                PauseLocationUpdatesAutomatically = false,
                DeferralDistanceMeters = MinimumDistanceMeters,
                DeferralTime = MinimumTimeSpan,
                ListenForSignificantChanges = isBackground
            };
        }

        void OnPositionChanged(object sender, PositionEventArgs e)
        {
            var message = $"OnPositionChanged Latitude:{e.Position.Latitude}, Longitude:{e.Position.Longitude}";
            System.Diagnostics.Debug.WriteLine(message);

            Task.Run(async () =>
            {
                await LogService.WriteAsync(message);
            }).ContinueWith((arg) =>
            {
                System.Diagnostics.Debug.WriteLineIf(arg.IsFaulted, arg.Exception);
            });
        }

        void OnPositionError(object sender, PositionErrorEventArgs e)
        {
            GeolocationListener.Current.StopListeningAsync();
            var message = $"OnPositionError Latitude:{e.Error}";
            System.Diagnostics.Debug.WriteLine(message);

            Task.Run(async () =>
            {
                await LogService.WriteAsync(message);
            }).ContinueWith((arg) =>
            {
                System.Diagnostics.Debug.WriteLineIf(arg.IsFaulted, arg.Exception);
            });
        }

        Task DisplayErrorDialogAsync(string message)
        {
            return Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert(ErrorTitle, message, "OK");
            });
        }
    }
}
