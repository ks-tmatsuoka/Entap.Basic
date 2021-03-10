using System;
using Entap.Basic;
using Entap.Basic.Forms;

namespace SHIRO.CO
{
    public class PageNavigator : BasicPageNavigator
    {
        public override void SetHomePage()
        {
            PageManager.Navigation.SetNavigationMainPage<HomePage>(new HomePageViewModel());
        }
    }
}
