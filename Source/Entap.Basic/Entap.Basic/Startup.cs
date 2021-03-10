using System;
using Entap.Basic.Auth.Abstractions;
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

        static Startup()
        {
            ServiceCollection = new ServiceCollection();
            AddDefaultService();
        }

        static void AddDefaultService()
        {
            ServiceCollection.AddTransient<ISplashUseCase, BasicSplashUseCase>();
            ServiceCollection.AddTransient<IGuideUseCase, BasicGuideUseCase>();
            ServiceCollection.AddTransient<IGuideUseCase, BasicGuideUseCase>();
            ServiceCollection.AddTransient<IConfirmTermsUseCase, BasicConfirmTermsUseCase>();
            ServiceCollection.AddTransient<ITermsUseCase, BasicTermsUseCase>();
            ServiceCollection.AddTransient<ILoginPortalUseCase, BasicLoginPortalUseCase>();
            ServiceCollection.AddTransient<ISignUpUseCase, BasicSignUpUseCase>();
        }
    }
}
