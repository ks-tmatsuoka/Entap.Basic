using System;
using Entap.Basic;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Splash;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace SHIRO.CO
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Core.Init(this);
            
            ConfigureServices();
            // ToDo
            if (Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance.CurrentUser is null)
                PageManager.Navigation.SetMainPage<SplashPage>(new SplashPageViewModel());
            else
                PageManager.Navigation.SetNavigationMainPage<HomePage>(new HomePageViewModel());
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
            ConfigureEntapBasicServices();
        }

        void ConfigureEntapBasicServices()
        {
            Startup.ConfigurePageNavigator<PageNavigator>();
            Startup.ServiceCollection.AddTransient<IPasswordAuthService, PasswordAuthService>();

            Startup.ConfigureUseCase<ISplashPageUseCase, SplashPageUseCase>();
        }
    }
}
