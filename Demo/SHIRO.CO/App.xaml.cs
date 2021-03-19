using System;
using Entap.Basic;
using Entap.Basic.Forms;
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
            
            ConfigureServices();
            // ToDo
            if (Plugin.FirebaseAuth.CrossFirebaseAuth.Current.Instance.CurrentUser is null)
                BasicStartup.PageNavigator.SetSplashPageAsync();
            else
                BasicStartup.PageNavigator.SetHomePageAsync();
            //PageManager.Navigation.SetNavigationMainPage<LoginPortalPage>(new LoginPortalPageViewModel());

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
            BasicStartup.ConfigurePageNavigator<PageNavigator>();
            BasicStartup.ConfigureAuthService<PasswordAuthService>();
        }
    }
}
