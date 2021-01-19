using System;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Sample
{
    public class MainPageViewModel : PageViewModelBase
    {
        public MainPageViewModel()
        {
        }

        public ProcessCommand PageManagerCommand =>
            new ProcessCommand(() => PageManager.Navigation.PushAsync<MyTabbedPage>(new MyTabbedPageViewModel()));
    }
}
