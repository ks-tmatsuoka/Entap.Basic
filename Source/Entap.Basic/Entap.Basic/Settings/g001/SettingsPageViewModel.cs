using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Settings
{
    public class SettingsPageViewModel : PageViewModelBase
    {
        readonly ISettingsPageUseCase _useCase;
        public SettingsPageViewModel()
        {
            _useCase = Startup.ServiceProvider.GetService<ISettingsPageUseCase>();
            SetPageLifeCycle(_useCase);
        }
    }
}
