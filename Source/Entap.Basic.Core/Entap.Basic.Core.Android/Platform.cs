using System;
using Android.App;
using Android.Runtime;

namespace Entap.Basic.Core.Android
{
    [Preserve(AllMembers = true)]
    public static class Platform
    {
        public static Activity Activity { get; private set; }

        public static void Init(Activity activity)
        {
            Activity = activity;
            PlatformHandler.Handle(activity);
        }
    }
}
