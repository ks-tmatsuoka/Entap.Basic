using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Auth
{
    public class ResetPasswordPageViewModel : PageViewModelBase
    {
        readonly IResetPasswordPageUseCase _useCase;
        public ResetPasswordPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<IResetPasswordPageUseCase>();
            SetPageLifeCycle(_useCase);
        }
    }
}
