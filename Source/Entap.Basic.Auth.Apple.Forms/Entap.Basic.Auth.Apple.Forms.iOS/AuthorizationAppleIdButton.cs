using System;
using AuthenticationServices;
using UIKit;

namespace Entap.Basic.Auth.Apple.Forms.iOS
{
    public class AuthorizationAppleIdButton : UIButton
    {
        new public EventHandler TouchUpInside;
        public ASAuthorizationAppleIdButton Button;

        public AuthorizationAppleIdButton(ASAuthorizationAppleIdButtonType type = ASAuthorizationAppleIdButtonType.Default, ASAuthorizationAppleIdButtonStyle style = ASAuthorizationAppleIdButtonStyle.Black)
        {
            Button = new ASAuthorizationAppleIdButton(type, style);
            Button.TouchUpInside += OnTouchUpInside;
            AddSubview(Button);
            Button.TranslatesAutoresizingMaskIntoConstraints = false;

            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[]
            {
                Button.TopAnchor.ConstraintEqualTo(TopAnchor),
                Button.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                Button.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                Button.TrailingAnchor.ConstraintEqualTo(TrailingAnchor)
            });
        }

        private void OnTouchUpInside(object sender, EventArgs e)
        {
            TouchUpInside.Invoke(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (Button is not null)
                Button.TouchUpInside -= OnTouchUpInside;

            base.Dispose(disposing);
        }
    }
}
