using System;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Splash;
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
            PageManager.Navigation.SetNavigationMainPage<MainPage>(new MainPageViewModel());
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
