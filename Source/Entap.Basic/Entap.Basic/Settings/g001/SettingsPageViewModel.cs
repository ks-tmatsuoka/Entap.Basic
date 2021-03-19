using System;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Settings
{
    public class SettingsPageViewModel : PageViewModelBase
    {
        readonly ISettingsPageUseCase _useCase;
        public SettingsPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<ISettingsPageUseCase>();
            SetPageLifeCycle(_useCase);
        }

        public Command SignOutCommand => new Command(() => _useCase.SignOut());
    }
}
