using System;
using System.Linq;
using AuthenticationServices;
using Entap.Basic.Auth.Apple.Abstract;

namespace Entap.Basic.Auth.Apple.iOS
{
    public static class AuthorizationScopeExtensions
    {
        public static ASAuthorizationScope[] ToASAuthorizationScopes(this AuthorizationScope[] scopes)
            => scopes
                .Select((scope) => scope.ToASAuthorizationScope())
                .ToArray();

        static ASAuthorizationScope ToASAuthorizationScope(this AuthorizationScope scope)
            => scope switch
            {
                AuthorizationScope.FullName => ASAuthorizationScope.FullName,
                AuthorizationScope.Email => ASAuthorizationScope.Email,
                _ => throw new NotImplementedException(),
            };
    }
}
