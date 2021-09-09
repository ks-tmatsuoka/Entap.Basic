using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Sample
{
    public class PageManagerPageViewModel : PageViewModelBase
    {
        public PageManagerPageViewModel()
        {
        }

        public ProcessCommand PushCommand => PushCommand<PageManagerPage>(new PageManagerPageViewModel());

        public ProcessCommand PushModalCommand => PushModalCommand<PageManagerPage>(new PageManagerPageViewModel());

        public ProcessCommand PushNavigationModalCommand => PushNavigationModalCommand<PageManagerPage>(new PageManagerPageViewModel(), hasNavigationCloseButton: true);
    }
}
