using System;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class TermsPageViewModel : PageViewModelBase
    {
        readonly ITermsUseCase _termsUseCase;
        public TermsPageViewModel()
        {
            _termsUseCase = Startup.ServiceProvider.GetService<ITermsUseCase>() ?? new BasicTermsUseCase();
            SetPageLifeCycle(_termsUseCase);
        }

        public Command CloseCommand => new Command(() =>
        {
            _termsUseCase.Close();
        });
    }
}
