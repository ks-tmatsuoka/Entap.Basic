using System;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class TermsPageViewModel : PageViewModelBase
    {
        readonly ITermsPageUseCase _useCase;
        public TermsPageViewModel()
        {
            _useCase = BasicStartup.GetUseCase<ITermsPageUseCase>();
            SetPageLifeCycle(_useCase);
        }

        public Command CloseCommand => new Command(() =>
        {
            _useCase.Close();
        });
    }
}
