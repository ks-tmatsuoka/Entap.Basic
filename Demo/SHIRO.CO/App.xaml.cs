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
            BasicStartup.PageNavigator.SetStartUpPageAsync();
            Entap.Basic.Basic.Init();
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
            AuthManager.ConfigureServices();
            ConfigureEntapBasicServices();
            
        }

        void ConfigureEntapBasicServices()
        {
            BasicStartup.ConfigurePageNavigator<PageNavigator>();
            BasicStartup.ConfigureAuthManagr<AuthManager>();
        }
    }
}
