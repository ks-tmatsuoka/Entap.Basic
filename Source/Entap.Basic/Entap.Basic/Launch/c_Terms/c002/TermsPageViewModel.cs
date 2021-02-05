using System;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class TermsPageViewModel : PageViewModelBase
    {
        ITermsUseCase _termsUseCase;
        public TermsPageViewModel(ITermsUseCase termsUseCase, IPageLifeCycle pageLifeCycle = null) : base(pageLifeCycle)
        {
            _termsUseCase = termsUseCase;
        }

        public Command CloseCommand => new Command(() =>
        {
            _termsUseCase.Close();
        });
    }
}
