using System;
using System.Threading.Tasks;
using Entap.Basic.Auth.Abstractions;

namespace Entap.Basic.Auth.Abstractions
{
    public interface IAuthManager
    {
        bool IsPasswordAuthSupported { get; }
        IPasswordAuthService PasswordAuthService { get; }

        bool IsTwitterAuthSupported { get; }
        ISnsAuthService TwitterAuthService { get; }

        bool IsFacebookAuthSupported { get; }
        ISnsAuthService FacebookAuthService { get; }

        bool IsLineAuthSupported { get; }
        ISnsAuthService LineAuthService { get; }

        bool IsGoogleAuthSupported { get; }
        ISnsAuthService GoogleAuthService { get; }

        bool IsAppleAuthSupported { get; }
        ISnsAuthService AppleAuthService { get; }

        Task SignOutAsync();
    }
}
