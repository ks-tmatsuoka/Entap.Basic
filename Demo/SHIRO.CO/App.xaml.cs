using System;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Splash;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SHIRO.CO
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Core.Init(this);
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
