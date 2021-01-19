using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    public class TabbedPageNavigationBehavior : BindableBehavior<TabbedPage>
    {
        TabbedPage _tabbedPage;
        Page _oldPage;
        protected override void OnAttachedTo(TabbedPage bindable)
        {
            base.OnAttachedTo(bindable);

            _tabbedPage = bindable;
            _oldPage = bindable.CurrentPage;
            AssociatedObject.CurrentPageChanged += OnCurrentPageChanged;
        }
        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            AssociatedObject.CurrentPageChanged -= OnCurrentPageChanged;
            _tabbedPage = null;
            _oldPage = null;
            base.OnDetachingFrom(bindable);
        }

        void OnCurrentPageChanged(object sender, EventArgs e)
        {
            if (_oldPage != null)
                ((PageViewModelBase)_oldPage.BindingContext)?.OnExit();

            ((PageViewModelBase)_tabbedPage.CurrentPage?.BindingContext)?.OnEntry();

            _oldPage = _tabbedPage.CurrentPage;
        }
        protected override void OnBindingContextChanged()
        {
            // TabbedPageの初期表示時に、CurrentPageのBindingContextがNullのため
            // BindingContextChange後にOnEntryを実行
            ((PageViewModelBase)_tabbedPage.CurrentPage?.BindingContext)?.OnEntry();
        }
    }
}