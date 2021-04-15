using System;
using Entap.Basic.Auth.Line;

namespace Entap.Basic.Firebase.Auth.Line
{
    public class LineAuthParameter
    {
        public LineAuthParameter()
        {
        }

        public LineAuthParameter(string clientId, string clientSecret, string scope, string redirectUri)
        {
            AuthRequest = new LineAuthRequest(clientId, redirectUri, scope);
            AccessTokenRequest = new LineAccessTokenRequest(redirectUri, clientId, clientSecret);
        }

        public LineAuthRequest AuthRequest { get; set; }

        public LineAccessTokenRequest AccessTokenRequest { get; set; }
    }
}
