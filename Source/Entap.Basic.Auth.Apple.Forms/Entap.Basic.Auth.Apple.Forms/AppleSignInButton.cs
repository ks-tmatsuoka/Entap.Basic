using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Entap.Basic.Auth.Apple.Forms
{
    public class AppleSignInButton : View
    {
        public AppleSignInButton() : this(ButtonType.Default, ButtonStyle.Black)
        {
        }

        public AppleSignInButton(ButtonType buttonType, ButtonStyle buttonStyle)
        {
            ButtonType = buttonType;
            ButtonStyle = buttonStyle;
        }

        public ButtonType ButtonType;
        public ButtonStyle ButtonStyle;

        #region CornerRadius BindableProperty
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(AppleSignInButton),
            -1d,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((AppleSignInButton)bindable).CornerRadius = (double)newValue);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region Command BindableProperty
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(AppleSignInButton),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((AppleSignInButton)bindable).Command = (ICommand)newValue);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region CommandParameter BindableProperty
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(AppleSignInButton),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((AppleSignInButton)bindable).CommandParameter = (object)newValue);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        public event EventHandler Clicked;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendClicked()
        {
            if (!IsEnabled) return;

            Clicked?.Invoke(this, EventArgs.Empty);
            if (Command?.CanExecute(CommandParameter) == true)
                Command?.Execute(CommandParameter);
        }
    }
}
