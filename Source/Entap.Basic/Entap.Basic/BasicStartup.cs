using System;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Auth;
using Entap.Basic.Launch.Guide;
using Entap.Basic.Launch.LoginPortal;
using Entap.Basic.Launch.Splash;
using Entap.Basic.Launch.Terms;
using Entap.Basic.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Entap.Basic
{
    public static class BasicStartup
    {
        private static readonly ServiceCollection _serviceCollection;
        static BasicStartup()
        {
            _serviceCollection = new ServiceCollection();
            AddDefaultService();
        }

        static IServiceProvider ServiceProvider => _serviceCollection.BuildServiceProvider();

        #region PageNavigator
        /// <summary>
        /// PageNavigatorの登録
        /// </summary>
        /// <typeparam name="TImplementation">PageNavigatorの実装タイプ</typeparam>
        public static void ConfigurePageNavigator<TImplementation>()
            where TImplementation : class, IPageNavigator
        {
            _serviceCollection.AddSingleton<IPageNavigator, TImplementation>();
        }

        /// <summary>
        /// PageNavigator
        /// </summary>
        public static IPageNavigator PageNavigator => ServiceProvider.GetService<IPageNavigator>();
        #endregion

        #region AuthService
        /// <summary>
        /// PageNavigatorの登録
        /// </summary>
        /// <typeparam name="TImplementation">PageNavigatorの実装タイプ</typeparam>
        public static void ConfigureAuthService<TImplementation>()
            where TImplementation : class, IPasswordAuthService
        {
            _serviceCollection.AddSingleton<IPasswordAuthService, TImplementation>();
        }

        /// <summary>
        /// PageNavigator
        /// </summary>
        public static IPasswordAuthService AuthService => ServiceProvider.GetService<IPasswordAuthService>();
        #endregion

        #region UseCase
        /// <summary>
        /// UseCaseの登録
        /// </summary>
        /// <typeparam name="TService">UseCaseのサービスタイプ</typeparam>
        /// <typeparam name="TImplementation">UseCase実装タイプ</typeparam>
        public static void ConfigureUseCase<TService, TImplementation>()
            where TService : class, IPageLifeCycle
            where TImplementation : class, TService
        {
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        public static T GetUseCase<T>() where T : IPageLifeCycle
        {
            return ServiceProvider.GetService<T>();
        }
        #endregion

        static void AddDefaultService()
        {
            ConfigurePageNavigator<BasicPageNavigator>();

            ConfigureUseCase<ISplashPageUseCase, BasicSplashPageUseCase>();
            ConfigureUseCase<IGuidePageUseCase, BasicGuidePageUseCase>();
            ConfigureUseCase<IConfirmTermsPageUseCase, BasicConfirmTermsPageUseCase>();
            ConfigureUseCase<ITermsPageUseCase, BasicTermsPageUseCase>();
            ConfigureUseCase<ILoginPortalPageUseCase, BasicLoginPortalPageUseCase>();
            ConfigureUseCase<ISignUpPageUseCase, BasicSignUpPageUseCase>();
            ConfigureUseCase<IPasswordSignInPageUseCase, BasicPasswordSignInPageUseCase>();

            ConfigureUseCase<ISettingsPageUseCase, BasicSettingsPageUseCase>();
        }
    }
}
