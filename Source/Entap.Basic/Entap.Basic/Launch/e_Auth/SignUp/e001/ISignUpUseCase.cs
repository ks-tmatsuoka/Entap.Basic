using System;
namespace Entap.Basic.Launch.Auth
{
    public interface ISignUpUseCase
    {
        string ValidateMailAddress(string mailAddress);
        string ValidatePassword(string password);

        void SignUp(string mailAddress, string password);
    }
}
