using System;
using Android.App;
using Android.Runtime;

namespace Entap.Basic.Forms.Android
{
    [Preserve(AllMembers = true)]
    public　static class Platform
    {
        public static void Init(Activity activity)
        {
            Entap.Basic.Core.Android.Platform.Init(activity);
        }
    }
}
