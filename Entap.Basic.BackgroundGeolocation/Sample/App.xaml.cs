using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entap.Basic.BackgroundGeolocation;
using Entap.Basic.Forms;

namespace LRMS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Entap.Basic.Forms.Core.Init(this);
            ConfigureServices();

            PageManager.Navigation.SetNavigationMainPage<GeolocationTestPage>(new GeolocationTestPageViewModel());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void ConfigureServices()
        {
            Entap.Basic.Core.BasicStartup.Current.ConfigureGeolocationService<GeolocationService>();
        }
    }
}
