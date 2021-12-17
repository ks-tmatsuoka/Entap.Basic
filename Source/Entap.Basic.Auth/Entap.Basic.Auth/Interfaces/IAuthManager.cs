using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth.Abstractions
{
    public interface IAuthManager
    {
        bool IsPasswordAuthSupported { get; }
        IPasswordAuthService PasswordAuthService { get; }

        bool IsTwitterAuthSupported { get; }
        ITwitterAuthService TwitterAuthService { get; }

        bool IsFacebookAuthSupported { get; }
        IFacebookAuthService FacebookAuthService { get; }

        bool IsLineAuthSupported { get; }
        ILineAuthService LineAuthService { get; }

        bool IsGoogleAuthSupported { get; }
        IGoogleAuthService GoogleAuthService { get; }

        bool IsAppleAuthSupported { get; }
        IAppleAuthService AppleAuthService { get; }

        bool IsAnonymousAuthSupported { get; }
        IAnonymousAuthService AnonymousAuthService { get; }

        Task SignOutAsync();
    }
}
