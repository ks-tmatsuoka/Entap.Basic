using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Guide;

namespace Entap.Basic
{
    public class BasicPageNavigator : IPageNavigator
    {
        public virtual void SetHomePage()
        {
            throw new NotImplementedException();
        }

        public virtual Task SetGuidePageAsync()
        {
            // ToDo
            var contents = new List<GuideContent>()
            {
                new GuideContent { Title = "title 1", Description = "description 1", Next = "つぎへ" },
                new GuideContent { Title = "title 2", Description = "description 2", Next = "つぎへ" },
                new GuideContent { Title = "title 3", Description = "description 3", Next = "はじめる" }
            };
            return PageManager.Navigation.SetMainPage<GuidePage>(new GuidePageViewModel(contents));
        }
    }
}
