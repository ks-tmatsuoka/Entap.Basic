using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Sample
{
    public class AppleSignInPageViewModel : PageViewModelBase
    {
        public AppleSignInPageViewModel()
        {
        }

        public ProcessCommand ACommand => new ProcessCommand(async () =>
        {
            // ToDo
        });
    }
}
