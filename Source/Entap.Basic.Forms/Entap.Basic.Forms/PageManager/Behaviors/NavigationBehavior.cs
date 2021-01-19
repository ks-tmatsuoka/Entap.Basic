using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    /// <summary>
    /// NavigationPageのPoppedイベントを検出し、 OnEntry・OnDestroyを実行する。
    /// （NavigationBarの戻るボタンのクリック時等、コード制御出来ないためBehaviorで検出する。）
    /// </summary>
    public class NavigationBehavior : BindableBehavior<NavigationPage>
    {
        protected override void OnAttachedTo(NavigationPage bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject.Popped += OnPopped;
        }

        protected override void OnDetachingFrom(NavigationPage bindable)
        {
            AssociatedObject.Popped -= OnPopped;
            base.OnDetachingFrom(bindable);
        }

        void OnPopped(object sender, NavigationEventArgs e)
        {
            PageManager.Navigation.GetViewModelBase(AssociatedObject.CurrentPage)?.OnEntry();
            if (AssociatedObject.CurrentPage is TabbedPage tabbedPage)
                PageManager.Navigation.GetViewModelBase(tabbedPage.CurrentPage)?.OnEntry();
            PageManager.Navigation.OnPagePopped(e.Page);
        }
    }
}
