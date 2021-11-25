using System;
using Foundation;

namespace Entap.Basic.Firebase.Auth.iOS
{
    [Preserve(AllMembers = true)]
    public class Platform
    {
        public static void Init()
        {
            if (global::Firebase.Core.App.DefaultInstance is null)
                global::Firebase.Core.App.Configure();
        }
    }
}
