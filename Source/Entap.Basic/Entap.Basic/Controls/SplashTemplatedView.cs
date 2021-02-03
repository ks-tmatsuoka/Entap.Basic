using System;
using Xamarin.Forms;

namespace Entap.Basic.Controls
{
    public class SplashTemplatedView : TemplatedView
    {
        public SplashTemplatedView()
        {
        }

        #region IsLoading BindableProperty
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(SplashTemplatedView),
            false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((SplashTemplatedView)bindable).IsLoading = (bool)newValue);

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        #endregion
    }
}
