using System;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.LoginPortal
{
    public class LoginPortalPageViewModel : PageViewModelBase
    {
        readonly ILoginPortalPageUseCase _useCase;
        public LoginPortalPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<ILoginPortalPageUseCase>();
            SetPageLifeCycle(_useCase);
        }

        public Command TwitterCommand => new Command(() => _useCase.SignInWithTwitter());

        public Command FacebookCommand => new Command(() => _useCase.SignInWithFacebook());

        public Command LineCommand => new Command(() => _useCase.SignInWithLine());

        public Command GoogleCommand => new Command(() => _useCase.SignInWithGoogle());

        public Command PasswordSignInCommand => new Command(() => _useCase.SignInWithEmailAndPassword());

        public Command SignUpCommand => new Command(() => _useCase.SignUp());

        public Command SkipCommand => new Command(() => _useCase.SkipAuth());
    }
}
