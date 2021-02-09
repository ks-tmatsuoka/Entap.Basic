using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Entap.Basic.Controls
{
    public partial class TitledPasswordEntry : TitledEntry
    {
        public TitledPasswordEntry()
        {
            InitializeComponent();

            passwordEntry.Focused += OnEntryFocused;
            passwordEntry.Unfocused += OnEntryFocused;
            visibleButton.Clicked += OnVisibleButtonClicked;
        }

        void OnEntryFocused(object sender, FocusEventArgs e)
        {
            OnPropertyChanged(nameof(IsEntryFocused));
        }
        public new bool IsEntryFocused => passwordEntry?.IsFocused ?? false;

        void OnVisibleButtonClicked(object sender, EventArgs e)
        {
            IsPassword = !IsPassword;
        }

        #region IsPassword BindableProperty
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(TitledPasswordEntry),
            true,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledPasswordEntry)bindable).IsPassword = (bool)newValue);

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }
        #endregion
    }
}
