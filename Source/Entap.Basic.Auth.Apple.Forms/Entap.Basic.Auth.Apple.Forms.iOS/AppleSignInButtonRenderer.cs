using System;
using System.ComponentModel;
using AuthenticationServices;
using Entap.Basic.Auth.Apple.Forms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppleSignInButton), typeof(Entap.Basic.Auth.Apple.Forms.iOS.AppleSignInButtonRenderer))]
namespace Entap.Basic.Auth.Apple.Forms.iOS
{
    public class AppleSignInButtonRenderer : ViewRenderer<AppleSignInButton, UIView>
    {
        AuthorizationAppleIdButton _button;
        protected override void OnElementChanged(ElementChangedEventArgs<AppleSignInButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is not null)
            {
                SetNativeControl();
                UpdateCornerRadius();
            }
            if (e.OldElement is not null)
            {
                _button.TouchUpInside -= OnTouchUpInside;
                _button.Dispose();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Button.CornerRadiusProperty.PropertyName)
                UpdateCornerRadius();
        }

        void SetNativeControl()
        {
            _button = new AuthorizationAppleIdButton(
                ToASAuthorizationAppleIdButtonType(Element.ButtonType),
                ToASAuthorizationAppleIdButtonStyle(Element.ButtonStyle));
            _button.TouchUpInside += OnTouchUpInside;
            SetNativeControl(_button);
        }

        private void OnTouchUpInside(object sender, EventArgs e)
        {
            Element.SendClicked();
        }

        void UpdateCornerRadius()
        {
            if (Element.CornerRadius < 0) return;

            _button.Button.CornerRadius = (float)Element.CornerRadius;
        }

        ASAuthorizationAppleIdButtonType ToASAuthorizationAppleIdButtonType(ButtonType buttonType) =>
            buttonType switch
            {
                ButtonType.SignIn => ASAuthorizationAppleIdButtonType.SignIn,
                ButtonType.SignUp => ASAuthorizationAppleIdButtonType.SignUp,
                ButtonType.Continue => ASAuthorizationAppleIdButtonType.Continue,
                _ => throw new ArgumentOutOfRangeException(nameof(ButtonType))
            };

        ASAuthorizationAppleIdButtonStyle ToASAuthorizationAppleIdButtonStyle(ButtonStyle buttonStyle) =>
            buttonStyle switch
            {
                ButtonStyle.Black => ASAuthorizationAppleIdButtonStyle.Black,
                ButtonStyle.White => ASAuthorizationAppleIdButtonStyle.White,
                ButtonStyle.WhiteOutline => ASAuthorizationAppleIdButtonStyle.WhiteOutline,
                _ => throw new ArgumentOutOfRangeException(nameof(ButtonStyle))
            };
    }
 }
