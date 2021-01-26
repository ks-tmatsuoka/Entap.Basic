using System;
using System.Collections.Generic;
using Entap.Basic.Forms;
using Entap.Basic.Forms.Launch.Guide;
using Entap.Basic.SQLite;
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

        public ProcessCommand SQLiteCommand =>
            //new ProcessCommand(() => PageManager.Navigation.PushAsync<SQLitePage>(new SQLitePageViewModel()));
            new ProcessCommand(() => PageManager.Navigation.PushAsync<SQLitePage>(new SQLitePageViewModelAsync()));

        public ProcessCommand GuideCommand =>
            new ProcessCommand(async () =>
            {
                var pageContent = new GuidePageContent
                {
                    BackgroundImage = "",
                    Contents = new List<GuideContent>()
                    {
                        new GuideContent { Title = "title 1", Description = "description 1", Next = "つぎへ" },
                        new GuideContent { Title = "title 2", Description = "description 2", Next = "つぎへ" },
                        new GuideContent { Title = "title 3", Description = "description 3", Next = "はじめる" }
                    }
                };
                await PageManager.Navigation.PushAsync<GuidePage>(new GuidePageViewModel(pageContent, new GuideUseCase()));
            });
    }
}
