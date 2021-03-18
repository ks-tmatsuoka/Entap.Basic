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
    public static class Startup
    {
        public static ServiceCollection ServiceCollection { get; set; }
        public static IServiceProvider ServiceProvider => ServiceCollection.BuildServiceProvider();

        /// <summary>
        /// PageNavigatorの登録
        /// </summary>
        /// <typeparam name="TImplementation">PageNavigatorの実装タイプ</typeparam>
        public static void ConfigurePageNavigator<TImplementation>()
            where TImplementation : class, IPageNavigator
        {
            ServiceCollection.AddSingleton<IPageNavigator, TImplementation>();
        }

        /// <summary>
        /// UseCaseの登録
        /// </summary>
        /// <typeparam name="TService">UseCaseのサービスタイプ</typeparam>
        /// <typeparam name="TImplementation">UseCase実装タイプ</typeparam>
        public static void ConfigureUseCase<TService, TImplementation>()
            where TService : class, IPageLifeCycle
            where TImplementation : class, TService
        {
            ServiceCollection.AddTransient<TService, TImplementation>();
        }

        static Startup()
        {
            ServiceCollection = new ServiceCollection();
            AddDefaultService();
        }    

        static void AddDefaultService()
        {
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
