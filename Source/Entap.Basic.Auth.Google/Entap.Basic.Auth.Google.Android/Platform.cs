using System;
using Android.App;
using Android.Content;
using Android.Runtime;

namespace Entap.Basic.Auth.Google.Android
{
    [Preserve(AllMembers = true)]
    public static class Platform
    {
        internal static bool Initialized;

        internal static Context Context;
        internal static string AuthClinetId;
        internal static int RequestCode;
        public static void Init(Context context, string authClinetId, int requestCode)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(authClinetId))
                throw new ArgumentNullException(nameof(authClinetId));

            Context = context;
            AuthClinetId = authClinetId;
            RequestCode = requestCode;

            Initialized = true;
        }
    }
}
