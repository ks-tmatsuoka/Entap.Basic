using System;
using Xamarin.Forms.Internals;

namespace Entap.Basic.iOS
{
    [Preserve(AllMembers = true)]
    public static class Platform
    {
        public static void Init()
        {
            Entap.Basic.Forms.iOS.Platform.Init();
        }
    }
}
