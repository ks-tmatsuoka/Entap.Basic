using System;
using Foundation;

namespace Entap.Basic.Firebase.Auth.iOS
{
    [Preserve(AllMembers = true)]
    public class Platform
    {
        public static void Init()
        {
            global::Firebase.Core.App.Configure();
        }
    }
}
