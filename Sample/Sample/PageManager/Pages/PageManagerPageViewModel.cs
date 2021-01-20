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

        public ProcessCommand PushCommand =>
            new ProcessCommand(() => PageManager.Navigation.PushAsync<PageManagerPage>(new PageManagerPageViewModel()));

        public ProcessCommand PushModalCommand =>
            new ProcessCommand(() => PageManager.Navigation.PushModalAsync<PageManagerPage>(new PageManagerPageViewModel()));

        public ProcessCommand PushNavigationModalCommand =>
            new ProcessCommand(() => PageManager.Navigation.PushNavigationModalAsync<PageManagerPage>(new PageManagerPageViewModel(), hasNavigationCloseButton:true));
    }
}
