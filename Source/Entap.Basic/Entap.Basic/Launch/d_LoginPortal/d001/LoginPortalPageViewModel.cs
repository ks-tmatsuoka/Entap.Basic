using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Launch.LoginPortal
{
    public class LoginPortalPageViewModel : PageViewModelBase
    {
        readonly ILoginPortalPageUseCase _useCase;
        public LoginPortalPageViewModel()
        {
            _useCase = Startup.ServiceProvider.GetService<ILoginPortalPageUseCase>();
            SetPageLifeCycle(_useCase);
        }

        public Command PasswordSignInCommand => new Command(() => _useCase.SignInWithEmailAndPassword());

        public Command SignUpCommand => new Command(() => _useCase.SignUp());

        public Command SkipCommand => new Command(() => _useCase.SkipAuth());
    }
}
