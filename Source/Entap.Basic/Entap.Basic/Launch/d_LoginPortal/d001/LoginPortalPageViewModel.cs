using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Launch.LoginPortal
{
    public class LoginPortalPageViewModel : PageViewModelBase
    {
        readonly ILoginPortalUseCase _loginPortalUseCase;
        public LoginPortalPageViewModel()
        {
            _loginPortalUseCase = Startup.ServiceProvider.GetService<ILoginPortalUseCase>();
            SetPageLifeCycle(_loginPortalUseCase);
        }

        public Command SignUpCommand => new Command(() => _loginPortalUseCase.SignUp());

        public Command SkipCommand => new Command(() => _loginPortalUseCase.SkipAuth());
    }
}
