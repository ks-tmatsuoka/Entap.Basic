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
        readonly ITermsPageUseCase _useCase;
        public TermsPageViewModel()
        {
            _useCase = Startup.ServiceProvider.GetService<ITermsPageUseCase>();
            SetPageLifeCycle(_useCase);
        }

        public Command CloseCommand => new Command(() =>
        {
            _useCase.Close();
        });
    }
}
