using System;
namespace Entap.Basic.Auth.Google
{
    public class Authentication
    {
        public Authentication()
        {
        }

        public string AccessToken { get; set; }

        public DateTime AccessTokenExpirationDate { get; set; }

        public string IdToken { get; set; }

        public DateTime IdTokenExpirationDate { get; set; }

        public string RefreshToken { get; set; }
    }
}
