using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationServices;
using Foundation;
#if __IOS__
using SafariServices;
#endif
using UIKit;
using WebKit;
#region Fix#1242
using Xamarin.Essentials;
using System.Diagnostics;
#endregion

namespace Entap.Basic.Auth.Line.iOS
{
    /// <summary>
    /// Web認証処理
    /// Xamrin.Essentials.WebAuthenticatorの不具合対応(Ver1.7.0)
    /// https://github.com/xamarin/Essentials/issues/1242 
    /// https://github.com/xamarin/Essentials/blob/main/Xamarin.Essentials/WebAuthenticator/WebAuthenticator.ios.tvos.cs
    /// </summary>
    public static class CustomWebAuthenticator
    {
#if __IOS__
        [System.Runtime.InteropServices.DllImport(ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Required for iOS Export")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "Required for iOS Export")]
        static extern void void_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

        const int asWebAuthenticationSessionErrorCodeCanceledLogin = 1;
        const string asWebAuthenticationSessionErrorDomain = "com.apple.AuthenticationServices.WebAuthenticationSession";

        const int sfAuthenticationErrorCanceledLogin = 1;
        const string sfAuthenticationErrorDomain = "com.apple.SafariServices.Authentication";
#endif

        static TaskCompletionSource<WebAuthenticatorResult> tcsResponse;
        static UIViewController currentViewController;
        static Uri redirectUri;

#if __IOS__
        static ASWebAuthenticationSession was;
        static SFAuthenticationSession sf;
#endif

        internal static async Task<WebAuthenticatorResult> PlatformAuthenticateAsync(WebAuthenticatorOptions webAuthenticatorOptions)
        {
            var url = webAuthenticatorOptions?.Url;
            var callbackUrl = webAuthenticatorOptions?.CallbackUrl;
            var prefersEphemeralWebBrowserSession = webAuthenticatorOptions?.PrefersEphemeralWebBrowserSession ?? false;

            if (!VerifyHasUrlSchemeOrDoesntRequire(callbackUrl.Scheme))
                throw new InvalidOperationException("You must register your URL Scheme handler in your app's Info.plist.");

            // Cancel any previous task that's still pending
            if (tcsResponse?.Task != null && !tcsResponse.Task.IsCompleted)
                tcsResponse.TrySetCanceled();

            tcsResponse = new TaskCompletionSource<WebAuthenticatorResult>();
            redirectUri = callbackUrl;
            var scheme = redirectUri.Scheme;

#if __IOS__
            static void AuthSessionCallback(NSUrl cbUrl, NSError error)
            {
                if (error == null)
                    OpenUrl(cbUrl);
                else if (error.Domain == asWebAuthenticationSessionErrorDomain && error.Code == asWebAuthenticationSessionErrorCodeCanceledLogin)
                    tcsResponse.TrySetCanceled();
                else if (error.Domain == sfAuthenticationErrorDomain && error.Code == sfAuthenticationErrorCanceledLogin)
                    tcsResponse.TrySetCanceled();
                else
                    tcsResponse.TrySetException(new NSErrorException(error));

                was = null;
                sf = null;
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(12, 0))
            {
                was = new ASWebAuthenticationSession(WebUtils.GetNativeUrl(url), scheme, AuthSessionCallback);

                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    // Fix#1242
                    //var ctx = new ContextProvider(Platform.GetCurrentWindow());
                    var ctx = new ContextProvider(CustomPlatform.GetCurrentWindow());

                    void_objc_msgSend_IntPtr(was.Handle, ObjCRuntime.Selector.GetHandle("setPresentationContextProvider:"), ctx.Handle);
                    was.PrefersEphemeralWebBrowserSession = prefersEphemeralWebBrowserSession;
                }
                else if (prefersEphemeralWebBrowserSession)
                {
                    ClearCookies();
                }

                // Fix#1242
                // https://github.com/xamarin/Essentials/issues/1242#issuecomment-619138608
                //using (was)
                //{
                //    was.Start();
                //    return await tcsResponse.Task;
                //}
                was.Start();
                var result = await tcsResponse.Task;
                was?.Cancel();
                was?.Dispose();
                was = null;
                return result;

            }

            if (prefersEphemeralWebBrowserSession)
                ClearCookies();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                sf = new SFAuthenticationSession(WebUtils.GetNativeUrl(url), scheme, AuthSessionCallback);
                using (sf)
                {
                    sf.Start();
                    return await tcsResponse.Task;
                }
            }

            // This is only on iOS9+ but we only support 10+ in Essentials anyway
            var controller = new SFSafariViewController(WebUtils.GetNativeUrl(url), false)
            {
                Delegate = new NativeSFSafariViewControllerDelegate
                {
                    DidFinishHandler = (svc) =>
                    {
                        // Cancel our task if it wasn't already marked as completed
                        if (!(tcsResponse?.Task?.IsCompleted ?? true))
                            tcsResponse.TrySetCanceled();
                    }
                },
            };

            currentViewController = controller;
            await Platform.GetCurrentUIViewController().PresentViewControllerAsync(controller, true);
#else
            var opened = UIApplication.SharedApplication.OpenUrl(url);
            if (!opened)
                tcsResponse.TrySetException(new Exception("Error opening Safari"));
#endif

            return await tcsResponse.Task;
        }

        static void ClearCookies()
        {
            NSUrlCache.SharedCache.RemoveAllCachedResponses();

#if __IOS__
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                WKWebsiteDataStore.DefaultDataStore.HttpCookieStore.GetAllCookies((cookies) =>
                {
                    foreach (var cookie in cookies)
                    {
                        WKWebsiteDataStore.DefaultDataStore.HttpCookieStore.DeleteCookie(cookie, null);
                    }
                });
            }
#endif
        }

        internal static bool OpenUrl(Uri uri)
        {
            // If we aren't waiting on a task, don't handle the url
            if (tcsResponse?.Task?.IsCompleted ?? true)
                return false;

            try
            {
                // If we can't handle the url, don't
                if (!WebUtils.CanHandleCallback(redirectUri, uri))
                    return false;

                currentViewController?.DismissViewControllerAsync(true);
                currentViewController = null;

                tcsResponse.TrySetResult(new WebAuthenticatorResult(uri));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        static bool VerifyHasUrlSchemeOrDoesntRequire(string scheme)
        {
            // iOS11+ uses sfAuthenticationSession which handles its own url routing
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return true;

            // Fix#1242
            //return AppInfo.VerifyHasUrlScheme(scheme);
            return CustomAppInfo.VerifyHasUrlScheme(scheme);
        }

#if __IOS__
        class NativeSFSafariViewControllerDelegate : SFSafariViewControllerDelegate
        {
            public Action<SFSafariViewController> DidFinishHandler { get; set; }

            public override void DidFinish(SFSafariViewController controller) =>
                DidFinishHandler?.Invoke(controller);
        }

        [ObjCRuntime.Adopts("ASWebAuthenticationPresentationContextProviding")]
        class ContextProvider : NSObject
        {
            public ContextProvider(UIWindow window) =>
                Window = window;

            public UIWindow Window { get; private set; }

            [Export("presentationAnchorForWebAuthenticationSession:")]
            public UIWindow GetPresentationAnchor(ASWebAuthenticationSession session)
                => Window;
        }
#endif
    }

    #region Fix#1242
    // https://github.com/xamarin/Essentials/blob/main/Xamarin.Essentials/Types/Shared/WebUtils.shared.cs
    public static class WebUtils
    {
        // https://github.com/xamarin/Essentials/blob/main/Xamarin.Essentials/Types/Shared/WebUtils.shared.cs#L45
        internal static bool CanHandleCallback(Uri expectedUrl, Uri callbackUrl)
        {
            if (!callbackUrl.Scheme.Equals(expectedUrl.Scheme, StringComparison.OrdinalIgnoreCase))
                return false;

            if (!string.IsNullOrEmpty(expectedUrl.Host))
            {
                if (!callbackUrl.Host.Equals(expectedUrl.Host, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        // https://github.com/xamarin/Essentials/blob/main/Xamarin.Essentials/Types/Shared/WebUtils.shared.cs#L60
        internal static Foundation.NSUrl GetNativeUrl(Uri uri)
        {
            try
            {
                return new Foundation.NSUrl(uri.OriginalString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to create NSUrl from Original string, trying Absolute URI: {ex.Message}");
                return new Foundation.NSUrl(uri.AbsoluteUri);
            }
        }
    }

    // https://github.com/xamarin/Essentials/blob/main/Xamarin.Essentials/Platform/Platform.ios.tvos.watchos.cs
    public static class CustomPlatform
    {
        // https://github.com/xamarin/Essentials/blob/83e667d0d158ea50aeb26082714cd11d9549506a/Xamarin.Essentials/Platform/Platform.ios.tvos.watchos.cs#L110
        internal static UIWindow GetCurrentWindow(bool throwIfNull = true)
        {
            var window = UIApplication.SharedApplication.KeyWindow;

            if (window != null && window.WindowLevel == UIWindowLevel.Normal)
                return window;

            if (window == null)
            {
                window = UIApplication.SharedApplication
                    .Windows
                    .OrderByDescending(w => w.WindowLevel)
                    .FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);
            }

            if (throwIfNull && window == null)
                throw new InvalidOperationException("Could not find current window.");

            return window;
        }
    }

    // https://github.com/xamarin/Essentials/blob/main/Xamarin.Essentials/AppInfo/AppInfo.ios.tvos.watchos.macos.cs
    public class CustomAppInfo
    {
        // https://github.com/xamarin/Essentials/blob/83e667d0d158ea50aeb26082714cd11d9549506a/Xamarin.Essentials/AppInfo/AppInfo.ios.tvos.watchos.macos.cs#L84
        internal static bool VerifyHasUrlScheme(string scheme)
        {
            var cleansed = scheme.Replace("://", string.Empty);
            var schemes = GetCFBundleURLSchemes().ToList();
            return schemes.Any(x => x != null && x.Equals(cleansed, StringComparison.InvariantCultureIgnoreCase));
        }

        internal static IEnumerable<string> GetCFBundleURLSchemes()
        {
            var schemes = new List<string>();

            NSObject nsobj = null;
            if (!NSBundle.MainBundle.InfoDictionary.TryGetValue((NSString)"CFBundleURLTypes", out nsobj))
                return schemes;

            var array = nsobj as NSArray;

            if (array == null)
                return schemes;

            for (nuint i = 0; i < array.Count; i++)
            {
                var d = array.GetItem<NSDictionary>(i);
                if (d == null || !d.Any())
                    continue;

                if (!d.TryGetValue((NSString)"CFBundleURLSchemes", out nsobj))
                    continue;

                var a = nsobj as NSArray;
                var urls = ConvertToIEnumerable<NSString>(a).Select(x => x.ToString()).ToArray();
                foreach (var url in urls)
                    schemes.Add(url);
            }

            return schemes;
        }

        static IEnumerable<T> ConvertToIEnumerable<T>(NSArray array)
            where T : class, ObjCRuntime.INativeObject
        {
            for (nuint i = 0; i < array.Count; i++)
                yield return array.GetItem<T>(i);
        }
    }
    #endregion
}
