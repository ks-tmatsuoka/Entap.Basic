using System;
using System.Threading.Tasks;
using Entap.Basic.Api;

namespace SHIRO.CO
{
    public class UserDataRepository : Entap.Basic.Firebase.Auth.UserDataRepository
    {
        public UserDataRepository() : base(new SecureStorageManager())
        {

        }

        public override Task SetAccessTokenAsync(ServerAccessToken accessToken)
        {
            BasicApiManager.Current.SetAuthorization(accessToken);
            return base.SetAccessTokenAsync(accessToken);
        }

        public override void RemoveAccessToken()
        {
            BasicApiManager.Current.ClearAuthorization();
            base.RemoveAccessToken();
        }
    }
}
