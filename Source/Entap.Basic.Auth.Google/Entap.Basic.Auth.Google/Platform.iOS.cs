using System;
using Foundation;
using UIKit;

namespace Entap.Basic.Auth.Google.iOS
{
    [Preserve(AllMembers = true)]
    public static class Platform
    {
        internal static bool Initialized;

        internal static string ClientId;
        internal static Func<UIViewController> GetViewController;
        public static void Init(string clientId, Func<UIViewController> getViewController)
        {
            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (getViewController is null)
                throw new ArgumentNullException(nameof(getViewController));

            ClientId = clientId;
            GetViewController = getViewController;

            Initialized = true;
        }
    }
}
