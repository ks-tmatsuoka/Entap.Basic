using System;
namespace Entap.Basic.Auth.Apple.Abstract
{
    /// <summary>
    /// AppleIdCredential
    /// https://developer.apple.com/documentation/authenticationservices/asauthorizationappleidcredential
    /// </summary>
    public class AppleIdCredential
    {
        public AppleIdCredential()
        {
        }

        public string IdToken { get;set; }

        public string Email { get;set; }

        public string UserId { get; set; }

        public PersonName FullName { get; set; }

        public UserDetectionStatus RealUserStatus { get; set; }
    }
}
