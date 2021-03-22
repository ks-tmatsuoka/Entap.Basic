using System;
using Android.App;
using Android.Runtime;

namespace Entap.Basic.Core.Android
{
    [Preserve(AllMembers = true)]
    public class Platform
    {
        public static void Init(Activity activity)
        {
            PlatformHandler.Handle(activity);
        }
    }
}
