using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Entap.Basic.Settings;
using Xamarin.Forms;

namespace SHIRO.CO
{
    public class HomePageViewModel : PageViewModelBase
    {
        public HomePageViewModel()
        {
        }

        public ProcessCommand SettingsCommand => new ProcessCommand(async () =>
        {
            await PageManager.Navigation.PushAsync<SettingsPage>(new SettingsPageViewModel());
        });
    }
}
