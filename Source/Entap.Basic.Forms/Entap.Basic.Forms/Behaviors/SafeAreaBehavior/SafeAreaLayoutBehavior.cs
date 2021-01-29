using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    /// <summary>
    /// Paddingを使用し、LayoutにSafeAreaを適用する
    /// </summary>
    public class SafeAreaLayoutBehavior : BindableBehavior<Layout>
    {
        Thickness? defaultPadding;
        public SafeAreaLayoutBehavior()
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

            SetSafePadding();
        }

        #region PositionFlags BindableProperty
        public static readonly BindableProperty PositionFlagsProperty = BindableProperty.Create(
            nameof(PositionFlags),
            typeof(ThicknessPositionFlags),
            typeof(SafeAreaLayoutBehavior),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is not SafeAreaLayoutBehavior behavior) return;
                behavior.PositionFlags = (ThicknessPositionFlags)newValue;
                behavior.SetSafePadding();
            });

        public ThicknessPositionFlags PositionFlags
        {
            get { return (ThicknessPositionFlags)GetValue(PositionFlagsProperty); }
            set { SetValue(PositionFlagsProperty, value); }
        }
        #endregion

        void SetSafePadding()
        {
            if (defaultPadding is null)
                defaultPadding = AssociatedObject.Padding;

            var padding = defaultPadding.Value.GetSafeAreaAppliedThickness(PositionFlags);
            if (padding is null) return;

            AssociatedObject.Padding = padding.Value;
        }
    }
}
