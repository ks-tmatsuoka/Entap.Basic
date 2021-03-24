using System;
using Android.App;
using Android.Runtime;

namespace Entap.Basic.Android
{
    [Preserve(AllMembers = true)]
    public static class Platform
    {
        public static void Init(Activity activity)
        {
            Entap.Basic.Forms.Android.Platform.Init(activity);
        }
    }
}
