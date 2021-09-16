using System;
using Entap.Basic.Auth.Apple.Abstract;
using Foundation;

namespace Entap.Basic.Auth.Apple.iOS
{
    public static class NSPersonNameComponentsExtensions
    {
        public static PersonName ToPersonName(this NSPersonNameComponents components)
        {
            return new PersonName
            {
                FamilyName = components.FamilyName,
                GivenName = components.GivenName,
                MiddleName = components.MiddleName,
                NamePrefix = components.NamePrefix,
                NameSuffix = components.NameSuffix,
                Nickname = components.Nickname,
            };
        }
    }
}
