using System;
using System.Threading.Tasks;
using Entap.Basic.BackgroundGeolocation;
using Entap.Basic.Forms;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace LRMS
{
    public class GeolocationTestPageViewModel : PageViewModelBase
    {
        public GeolocationTestPageViewModel()
        {
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public bool IsListening => CrossGeolocator.Current.IsListening;

        public void RefreshListeningStatus() => OnPropertyChanged(nameof(IsListening));

        public Command RefreshCommand => new Command(RefreshListeningStatus);

        public ProcessCommand SwitchCommand => new ProcessCommand(async () =>
        {
            if (IsListening)
                await GeolocationListener.Current.StopListeningAsync();
            else
                await GeolocationListener.Current.StartListeningAsync();
            await Task.Delay(500);
            RefreshListeningStatus();
        });

        public Command ClearLogCommand => new Command(() =>
        {
            LogService.Clear();
            Log = null;
        });

        public ProcessCommand ReadLogCommand => new ProcessCommand(async () =>
        {
            Log = await LogService.ReadAsync();
        });

        public ProcessCommand ShareLogCommand => new ProcessCommand(async () =>
        {
            await LogService.ShareAsync();
        });

        public string Log
        {
            get => _log;
            set => SetProperty(ref _log, value);
        }
        string _log;
    }
}
