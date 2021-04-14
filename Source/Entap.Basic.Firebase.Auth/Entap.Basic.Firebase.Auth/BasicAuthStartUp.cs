using System;
using Entap.Basic.Api;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic.Firebase.Auth
{
    public static class BasicAuthStartUp
    {
        private static readonly ServiceCollection _serviceCollection;
        static BasicAuthStartUp()
        {
            _serviceCollection = new ServiceCollection();
        }

        static IServiceProvider ServiceProvider => _serviceCollection.BuildServiceProvider();

        #region AuthApi
        /// <summary>
        /// AuthApiの登録
        /// </summary>
        /// <typeparam name="TImplementation">AuthApiの実装タイプ</typeparam>
        public static void ConfigureAuthApi<TImplementation>()
                where TImplementation : class, IAuthApi
        {
            _serviceCollection.AddSingleton<IAuthApi, TImplementation>();
        }

        /// <summary>
        /// AuthApi
        /// </summary>
        public static IAuthApi AuthApi => ServiceProvider.GetService<IAuthApi>();
        #endregion
    }
}
