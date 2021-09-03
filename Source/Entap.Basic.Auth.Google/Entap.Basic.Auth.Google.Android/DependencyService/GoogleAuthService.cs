using System;
using System.Threading.Tasks;
using Android.App;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common.Apis;
using Entap.Basic.Core.Android;
using Platform = Entap.Basic.Auth.Google.Android.Platform;

namespace Entap.Basic.Auth.Google
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public GoogleAuthService()
        {
            if (!Platform.Initialized)
                throw new InvalidOperationException("Please call Entap.Basic.Auth.Google.Android.Platform.Init() method.");
        }

        public async Task<GoogleSignInAccount> SingInAsync()
        {
            var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken(Platform.AuthClinetId)
                .Build();
            var client = GoogleSignIn.GetClient(Platform.Context, gso);

            var result = await StarterActivity.StartForResultAsync((Activity)Platform.Context, client.SignInIntent, Platform.RequestCode);
            var task = GoogleSignIn.GetSignedInAccountFromIntent(result.Data);
            var account = task.GetResult(Java.Lang.Class.FromType(typeof(ApiException)));
            return account as GoogleSignInAccount;
        }

        #region IGoogleAuthService
        public async Task<Authentication> AuthAsync()
        {
            var user = await SingInAsync();
            return new Authentication
            {
                IdToken = user.IdToken,
            };
        }

        public Task SignOutAsync()
        {
            var gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .Build();
            var client = GoogleSignIn.GetClient(Platform.Context, gso);

            return client.SignOutAsync();
        }
        #endregion
    }
}
