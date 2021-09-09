using System;
using System.Collections.Generic;
using Entap.Basic.Forms;
using Entap.Basic.SQLite;
using Xamarin.Forms;

namespace Sample
{
    public class MainPageViewModel : PageViewModelBase
    {
        public MainPageViewModel()
        {
        }

        public ProcessCommand SQLiteCommand =>
            //new ProcessCommand(() => PageManager.Navigation.PushAsync<SQLitePage>(new SQLitePageViewModel()));
            new ProcessCommand(() => PageManager.Navigation.PushAsync<SQLitePage>(new SQLitePageViewModelAsync()));
    }
}
