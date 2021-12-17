using System;
using System.Linq;
using Entap.Basic.Api;
using Entap.Basic.Auth;
using Entap.Basic.Auth.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic.Firebase.Auth
{
    public static class BasicFirebaseAuthStartUp
    {
        private static readonly ServiceCollection _serviceCollection;
        static BasicFirebaseAuthStartUp()
        {
            _serviceCollection = new ServiceCollection();
            ConfigureUserDataRepository<UserDataRepository>();
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


        #region AuthService
        public static void ConfigurePasswordAuthService<TImplementation>()
            where TImplementation : class, IPasswordAuthService
        {
            _serviceCollection.AddSingleton<IPasswordAuthService, TImplementation>();
        }
        /// <summary>
        /// PasswordAuthService
        /// </summary>
        public static IPasswordAuthService PasswordAuthService => ServiceProvider.GetService<IPasswordAuthService>();

        /// <summary>
        /// SnsAuthServiceの登録
        /// </summary>
        /// <typeparam name="TImplementation">SnsAuthServiceの実装タイプ</typeparam>
        // Twitter
        public static void ConfigureTwitterAuthService<TImplementation>()
            where TImplementation : class, ITwitterAuthService
        {
            _serviceCollection.AddSingleton<ITwitterAuthService, TImplementation>();
        }

        public static ITwitterAuthService TwitterAuthService => ServiceProvider.GetService<ITwitterAuthService>();

        // Facebook
        public static void ConfigureFacebookAuthService<TImplementation>()
            where TImplementation : class, IFacebookAuthService
        {
            _serviceCollection.AddSingleton<IFacebookAuthService, TImplementation>();
        }
        public static IFacebookAuthService FacebookAuthService => ServiceProvider.GetService<IFacebookAuthService>();

        // LINE
        public static void ConfigureLineAuthService<TImplementation>()
            where TImplementation : class, ILineAuthService
        {
            _serviceCollection.AddSingleton<ILineAuthService, TImplementation>();
        }
        public static ILineAuthService LineAuthService => ServiceProvider.GetService<ILineAuthService>();

        // Google
        public static void ConfigureGoogleAuthService<TImplementation>()
            where TImplementation : class, IGoogleAuthService
        {
            _serviceCollection.AddSingleton<IGoogleAuthService, TImplementation>();
        }
        public static IGoogleAuthService GoogleAuthService => ServiceProvider.GetService<IGoogleAuthService>();

        // Apple
        public static void ConfigureAppleAuthService<TImplementation>()
            where TImplementation : class, IAppleAuthService
        {
            _serviceCollection.AddSingleton<IAppleAuthService, TImplementation>();
        }
        public static IAppleAuthService AppleAuthService => ServiceProvider.GetService<IAppleAuthService>();

        // Anonymous
        public static void ConfigureAnonymousAuthService<TImplementation>()
            where TImplementation : class, IAnonymousAuthService
        {
            _serviceCollection.AddSingleton<IAnonymousAuthService, TImplementation>();
        }
        public static IAnonymousAuthService AnonymousAuthService => ServiceProvider.GetService<IAnonymousAuthService>();
        #endregion

        #region AuthErrorCallback
        public static void ConfigureAuthErrorCallback<TImplementation>()
            where TImplementation : class, IAuthErrorCallback
        {
            _serviceCollection.AddSingleton<IAuthErrorCallback, TImplementation>();
        }

        public static void ConfigurePasswordAuthErrorCallback<TImplementation>()
            where TImplementation : class, IPasswordAuthErrorCallback
        {
            _serviceCollection.AddSingleton<IPasswordAuthErrorCallback, TImplementation>();
        }
        #endregion

        #region IAccessTokenPreferencesService
        /// <summary>
        /// AuthApiの登録
        /// </summary>
        /// <typeparam name="TImplementation">AuthApiの実装タイプ</typeparam>
        public static void ConfigureAccessTokenPreferencesService<TImplementation>()
            where TImplementation : class, IAccessTokenPreferencesService
        {
            _serviceCollection.AddSingleton<IAccessTokenPreferencesService, TImplementation>();
        }
        #endregion

        #region UserDataRepository
        /// <summary>
        /// UserDataRepositoryの登録
        /// </summary>
        /// <typeparam name="TImplementation">AuthApiの実装タイプ</typeparam>
        public static void ConfigureUserDataRepository<TImplementation>()
            where TImplementation : class, IUserDataRepository
        {
            _serviceCollection.AddSingleton<IUserDataRepository, TImplementation>();
        }

        /// <summary>
        /// UserDataRepository
        /// </summary>
        public static IUserDataRepository UserDataRepository => ServiceProvider.GetService<IUserDataRepository>();
        #endregion
    }
}
