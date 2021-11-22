using System;
using System.Threading.Tasks;

namespace SHIRO.CO
{
    public class UserDataRepository : Entap.Basic.Firebase.Auth.UserDataRepository
    {
        public UserDataRepository() : base(new SecureStorageManager())
        {

        }

        public override Task SetAccessTokenAsync(string accessToken)
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
