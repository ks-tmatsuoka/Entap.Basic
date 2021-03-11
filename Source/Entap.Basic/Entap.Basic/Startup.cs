using System;
using Entap.Basic.Auth.Abstractions;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Auth;
using Entap.Basic.Launch.Guide;
using Entap.Basic.Launch.LoginPortal;
using Entap.Basic.Launch.Splash;
using Entap.Basic.Launch.Terms;
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
            ConfigureUseCase<ISplashUseCase, BasicSplashUseCase>();
            ConfigureUseCase<IGuideUseCase, BasicGuideUseCase>();
            ConfigureUseCase<IGuideUseCase, BasicGuideUseCase>();
            ConfigureUseCase<IConfirmTermsUseCase, BasicConfirmTermsUseCase>();
            ConfigureUseCase<ITermsUseCase, BasicTermsUseCase>();
            ConfigureUseCase<ILoginPortalUseCase, BasicLoginPortalUseCase>();
            ConfigureUseCase<ISignUpUseCase, BasicSignUpUseCase>();
            ConfigureUseCase<IPasswordSignInUseCase, BasicPasswordSingnInUseCase>();
        }
    }
}
