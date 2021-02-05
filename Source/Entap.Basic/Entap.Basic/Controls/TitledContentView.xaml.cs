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

        #region Text BindableProperty
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(TitledContentView),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).Text = (string)newValue);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region Message BindableProperty

        // HACK : MessageがNullの場合にLabelが非表示になるのを回避
        // フォントによりサイズが異なるため、サイズ指定はせずに空白文字を設定
        const string MessagePlaceholder = " ";

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            nameof(Message),
            typeof(string),
            typeof(TitledContentView),
            MessagePlaceholder,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((TitledContentView)bindable).Message = (string)newValue);

        public string Message
        {
            get
            {
                var message = (string)GetValue(MessageProperty);
                return  string.IsNullOrEmpty(message) ? MessagePlaceholder : message;
            }
            set { SetValue(MessageProperty, value); }
        }
        #endregion
    }
}
