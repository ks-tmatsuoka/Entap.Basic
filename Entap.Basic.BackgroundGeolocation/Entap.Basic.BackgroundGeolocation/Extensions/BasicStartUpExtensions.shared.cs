using System;
using Entap.Basic.Core;
using Microsoft.Extensions.DependencyInjection;
#if MONOANDROID
using Entap.Basic.Core.Android;
#endif
namespace Entap.Basic.BackgroundGeolocation
{
    public static class BasicStartUpExtensions
    {
        /// <summary>
        /// GeolocationServiceの登録
        /// </summary>
        /// <typeparam name="TImplementation">GeolocationServiceの実装タイプ</typeparam>
        /// <param name="startup">BasicStartup</param>
        public static void ConfigureGeolocationService<TImplementation>(this BasicStartup startup) where TImplementation : class, IGeolocationService
            => startup.ServiceCollection.AddSingleton<IGeolocationService, TImplementation>();

#if MONOANDROID
        /// <summary>
        /// GeolocationNotificationProviderの登録
        /// </summary>
        /// <typeparam name="TImplementation">GeolocationNotificationProviderの実装タイプ</typeparam>
        /// <param name="startup">BasicStartup</param>
        public static void ConfigureGeolocationNotificationProvider<TImplementation>(this BasicStartup startup) where TImplementation : class, IGeolocationNotificationProvider
            => startup.ServiceCollection.AddSingleton<IGeolocationNotificationProvider, TImplementation>();
#endif
    }
}
