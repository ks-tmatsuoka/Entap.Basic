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
                AuthorizationCode = new NSString(credential.AuthorizationCode, NSStringEncoding.UTF8).ToString(),
                IdToken = new NSString(credential.IdentityToken, NSStringEncoding.UTF8).ToString(),
                Email = credential.Email,
                UserId = credential.User,
                FullName = credential.FullName?.ToPersonName(),
                RealUserStatus = credential.RealUserStatus.ToUserDetectionStatus(),
            };
        }
    }
}
