using System;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Splash;
using Entap.Basic.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Core.Init(this);
            SQLiteConnectionManager.Init(new SQLiteConnectionService());
            PageManager.Navigation.SetMainPage<SplashPage>(new SplashPageViewModel(new SplashUseCase()));
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
    }
}
