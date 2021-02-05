using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    /// <summary>
    /// Marginを使用し、ViewにSafeAreaを適用する
    /// </summary>
    public class SafeAreaViewBehavior : BindableBehavior<View>
    {
        Thickness? defaultMargin;
        public SafeAreaViewBehavior()
        {
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            if (Device.RuntimePlatform == Device.Android)
            {
                OnDetachingFrom(bindable);
                return;
            }

            SetSafeMargin();
        }

        #region PositionFlags BindableProperty
        public static readonly BindableProperty PositionFlagsProperty = BindableProperty.Create(
            nameof(PositionFlags),
            typeof(ThicknessPositionFlags),
            typeof(SafeAreaViewBehavior),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is not SafeAreaViewBehavior behavior) return;
                behavior.PositionFlags = (ThicknessPositionFlags)newValue;
                behavior.SetSafeMargin();
            });

        public ThicknessPositionFlags PositionFlags
        {
            get { return (ThicknessPositionFlags)GetValue(PositionFlagsProperty); }
            set { SetValue(PositionFlagsProperty, value); }
        }
        #endregion

        void SetSafeMargin()
        {
            if (AssociatedObject is null) return;
            if (defaultMargin is null)
                defaultMargin = AssociatedObject.Margin;

            var margin = defaultMargin.Value.GetSafeAreaAppliedThickness(PositionFlags);
            if (margin is null) return;

            AssociatedObject.Margin = margin.Value;
        }
    }
}
