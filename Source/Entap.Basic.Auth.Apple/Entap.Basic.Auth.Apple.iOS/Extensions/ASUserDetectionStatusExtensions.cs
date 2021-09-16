using System;
using AuthenticationServices;
using Entap.Basic.Auth.Apple.Abstract;

namespace Entap.Basic.Auth.Apple.iOS
{
    public static class ASUserDetectionStatusExtensions
    {
        public static UserDetectionStatus ToUserDetectionStatus(this ASUserDetectionStatus status)
            => status switch
            {
                ASUserDetectionStatus.Unsupported => UserDetectionStatus.Unsupported,
                ASUserDetectionStatus.Unknown => UserDetectionStatus.Unknown,
                ASUserDetectionStatus.LikelyReal => UserDetectionStatus.LikelyReal,
                _ => throw new NotImplementedException(),
            };
    }
}
