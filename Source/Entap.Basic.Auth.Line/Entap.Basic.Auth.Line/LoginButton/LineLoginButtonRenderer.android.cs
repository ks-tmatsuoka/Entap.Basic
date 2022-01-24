using System;
using Entap.Basic.Auth.Line;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Com.Linecorp.Linesdk.Widget;
using Android.Content;
using AView = Android.Views.View;
using Com.Linecorp.Linesdk;
using Com.Linecorp.Linesdk.Internal;
using Com.Linecorp.Linesdk.Auth;
using Android.App;

[assembly: ExportRenderer(typeof(LineLoginButton), typeof(LineLoginButtonRenderer))]
namespace Entap.Basic.Auth.Line
{
    public class LineLoginButtonRenderer : ViewRenderer<LineLoginButton, AView>, ILoginListener
    {
        // https://github.com/line/line-sdk-android/blob/1420e9f91c744e57d33f0eede07dea4f580022a2/line-sdk/src/main/java/com/linecorp/linesdk/LoginDelegate.java#L29
        static LoginDelegateImpl _loginDelegate;
        Context _context;
        LoginButton _loginButton;

        public LineLoginButtonRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<LineLoginButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is not null)
            {
                _loginDelegate = new LoginDelegateImpl();
                SetNativeControl();
            }
            if (e.OldElement is not null)
            {
                _loginDelegate = null;
                _loginButton.Dispose();
            }
        }

        void SetNativeControl()
        {
            _loginButton = new LoginButton(_context);
            // https://developers.line.biz/ja/docs/android-sdk/integrate-line-login/#use-button
            _loginButton.SetChannelId(LineAuthService.ChannelId);
            _loginButton.EnableLineAppAuthentication(true);
            _loginButton.SetAuthenticationParams(
                new LineAuthenticationParams
                .Builder()
                .Scopes(LineAuthService.GetScopes(Element.Scopes))
                .Build());
            _loginButton.SetLoginDelegate(_loginDelegate);
            _loginButton.AddLoginListener(this);
            
            SetNativeControl(_loginButton);
        }

        internal static bool OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (_loginDelegate is null) return false;
            return _loginDelegate.OnActivityResult(requestCode, (int)resultCode, data);
        }

        #region ILoginListener
        public void OnLoginSuccess(LineLoginResult result)
        {
            Element.SendClicked(new LoginResult(result));
        }

        public void OnLoginFailure(LineLoginResult result)
        {
            Element.SendClicked(new LoginResult(result));
        }
        #endregion
    }
}
