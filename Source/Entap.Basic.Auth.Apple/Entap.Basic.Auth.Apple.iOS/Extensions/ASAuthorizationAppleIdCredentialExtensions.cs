using System;
using AuthenticationServices;
using Entap.Basic.Auth.Apple.Abstract;
using Foundation;

namespace Entap.Basic.Auth.Apple.iOS
{
    public static class ASAuthorizationAppleIdCredentialExtensions
    {
        public static AppleIdCredential ToAppleIdCredential(this ASAuthorizationAppleIdCredential credential)
        {
            return new AppleIdCredential()
            {
                Email = credential.Email,
                IdToken = new NSString(credential.IdentityToken, NSStringEncoding.UTF8).ToString(),
                UserId = credential.User,
                FullName = credential.FullName?.ToPersonName(),
            };
        }
    }
}
