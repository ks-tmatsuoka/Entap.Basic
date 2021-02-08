using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Entap.Basic.Controls
{
    public partial class TitledContentView : ContentView
    {
        public TitledContentView()
        {
            InitializeComponent();
        }

        #region Title BindableProperty
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(TitledContentView),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).Title = (string)newValue);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region IsRequired BindableProperty
        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(
            nameof(IsRequired),
            typeof(bool),
            typeof(TitledContentView),
            false,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).IsRequired = (bool)newValue);

        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }
        #endregion

        #region Placeholder BindableProperty
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(TitledContentView),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).Placeholder = (string)newValue);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        #endregion

        #region HasErrors BindableProperty
        public static readonly BindableProperty HasErrorsProperty = BindableProperty.Create(
            nameof(HasErrors),
            typeof(bool),
            typeof(TitledContentView),
            false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).HasErrors = (bool)newValue);

        public bool HasErrors
        {
            get { return (bool)GetValue(HasErrorsProperty); }
            set { SetValue(HasErrorsProperty, value); }
        }
        #endregion

        // HACK : MessageがNullの場合にLabelが非表示になるのを回避
        // フォントによりサイズが異なるため、サイズ指定はせずに空白文字を設定
        const string MessagePlaceholder = " ";

        #region DefaultMessage BindableProperty
        public static readonly BindableProperty DefaultMessageProperty = BindableProperty.Create(
            nameof(DefaultMessage),
            typeof(string),
            typeof(TitledContentView),
            MessagePlaceholder,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).DefaultMessage = (string)newValue);

        public string DefaultMessage
        {
            get
            {
                var message = (string)GetValue(DefaultMessageProperty);
                return string.IsNullOrEmpty(message) ? MessagePlaceholder : message;
            }
            set { SetValue(DefaultMessageProperty, value); }
        }
        #endregion

        #region ErrorMessage BindableProperty
        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
            nameof(ErrorMessage),
            typeof(string),
            typeof(TitledContentView),
            MessagePlaceholder,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).ErrorMessage = (string)newValue);

        public string ErrorMessage
        {
            get
            {
                var message = (string)GetValue(ErrorMessageProperty);
                return string.IsNullOrEmpty(message) ? MessagePlaceholder : message;
            }
            set { SetValue(ErrorMessageProperty, value); }
        }
        #endregion
    }
}
