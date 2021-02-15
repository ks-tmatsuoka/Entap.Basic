using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Entap.Basic.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;

namespace Entap.Basic.Controls
{
    public partial class TitledEntry : TitledContentView
    {
        public TitledEntry()
        {
            InitializeComponent();

            entry.Focused += OnEntryFocused;
            entry.Unfocused += OnEntryFocused;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == Entap.Basic.Forms.PlatformConfiguration.iOSSpecific.Entry.TextContentTypeProperty.PropertyName)
            {
                var textContentType = Forms.PlatformConfiguration.iOSSpecific.Entry.GetTextContentType(this);
                entry.On<iOS>().SetUITextContentType(textContentType);
            }
        }

        void OnEntryFocused(object sender, FocusEventArgs e)
        {
            OnPropertyChanged(nameof(IsEntryFocused));
        }
        public bool IsEntryFocused => entry.IsFocused;

        #region Text BindableProperty
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(TitledEntry),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledEntry)bindable).Text = (string)newValue);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region Keyboard BindableProperty
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            nameof(Keyboard),
            typeof(Keyboard),
            typeof(TitledEntry),
            Keyboard.Default,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledEntry)bindable).Keyboard = (Keyboard)newValue);

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }
        #endregion
    }
}