using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Guide;

namespace Entap.Basic.Launch.Splash
{
    public class SplashUseCase : ISplashUseCase
    {
        public SplashUseCase()
        {
        }

        public async Task LoadAsync()
        {
            await Task.Delay(3000);
            var contents = new List<GuideContent>()
            {
                new GuideContent { Title = "title 1", Description = "description 1", Next = "つぎへ" },
                new GuideContent { Title = "title 2", Description = "description 2", Next = "つぎへ" },
                new GuideContent { Title = "title 3", Description = "description 3", Next = "はじめる" }
            };
            await PageManager.Navigation.SetNavigationMainPage<GuidePage>(new GuidePageViewModel(contents, new GuideUseCase()));
        }
    }
}
