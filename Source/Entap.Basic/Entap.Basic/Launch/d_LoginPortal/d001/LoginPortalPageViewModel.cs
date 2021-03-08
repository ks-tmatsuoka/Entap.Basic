using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.LoginPortal
{
    public class LoginPortalPageViewModel : PageViewModelBase
    {
        ILoginPortalUseCase _loginPortalUseCase;
        public LoginPortalPageViewModel(ILoginPortalUseCase loginPortalUseCase)
        {
            _loginPortalUseCase = loginPortalUseCase;
        }

        public Command SkipCommand => new Command(() => _loginPortalUseCase.SkipAuth());
    }
}
