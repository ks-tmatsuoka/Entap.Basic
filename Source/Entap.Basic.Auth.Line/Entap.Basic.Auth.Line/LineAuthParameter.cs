using System;
namespace Entap.Basic.Auth.Line
{
    public class LineAuthParameter
    {
        readonly string _clientId;
        readonly string _clientSecret;
        readonly string _scope;
        readonly string _redirectUri;
        public LineAuthParameter(string clientId, string clientSecret, string scope, string redirectUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _scope = scope;
            _redirectUri = redirectUri;
        }

        public virtual LineAuthRequest CreateAuthRequest()
            => new(_clientId, _redirectUri, _scope);

        public LineAccessTokenRequest CreateAccessTokenRequest(string code)
            => new(code, _redirectUri, _clientId, _clientSecret, null);
    }
}