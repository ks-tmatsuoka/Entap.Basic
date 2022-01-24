using System;
using Entap.Basic.Auth.Line;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using LineSDKObjC;
using Foundation;

[assembly: ExportRenderer(typeof(LineLoginButton), typeof(LineLoginButtonRenderer))]
namespace Entap.Basic.Auth.Line
{
    public class LineLoginButtonRenderer : ViewRenderer<LineLoginButton, UIView>, ILineSDKLoginButtonDelegate
    {
        LineSDKLoginButton _loginButton;
        protected override void OnElementChanged(ElementChangedEventArgs<LineLoginButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is not null)
            {
                SetNativeControl();
            }
            if (e.OldElement is not null)
            {
                _loginButton.Dispose();
            }
        }

        void SetNativeControl()
        {
            // https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#use-button
            _loginButton = new LineSDKLoginButton();
            AddTouchUpInsideTarget();

            SetNativeControl(_loginButton.Button);
        }

        #region ILineSDKLoginButtonDelegate
        public void LoginButton(LineSDKLoginButton button, LineSDKLoginResult loginResult)
        {
            Element.SendClicked(new LoginResult(loginResult, null));
        }

        public void LoginButton(LineSDKLoginButton button, NSError error)
        {
            Element.SendClicked(new LoginResult(null, error));
        }

        public void LoginButtonDidStartLogin(LineSDKLoginButton button)
        {
            System.Diagnostics.Debug.WriteLine("LoginButtonDidStartLogin");
        }
        #endregion

        /// <summary>
        /// WeakLoginDelegateが動作しないため
        /// デフォルトのTouchUpInsideを無効化し、ログイン処理を実行する
        /// </summary>
        void AddTouchUpInsideTarget()
        {
            _loginButton.Button.RemoveTarget(null, null, UIControlEvent.TouchUpInside);
            _loginButton.Button.AddTarget(this, new ObjCRuntime.Selector("Login:"), UIControlEvent.TouchUpInside);
        }

        [Export("Login:")]
        void Login(UIButton sender)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var authService = new LineAuthService();
                var result = await authService.PlatformLoginAsync(Element.Scopes);
                Element.SendClicked(result);
            });
        }

    }
}
