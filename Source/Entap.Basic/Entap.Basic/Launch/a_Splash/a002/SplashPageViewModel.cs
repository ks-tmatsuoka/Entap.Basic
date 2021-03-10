using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Splash
{
    public class SplashPageViewModel : PageViewModelBase
    {
        readonly ISplashUseCase _splashUseCase;
        public SplashPageViewModel()
        {
            _splashUseCase = Startup.ServiceProvider.GetService<ISplashUseCase>();
            SetPageLifeCycle(_splashUseCase);

            IsLoading = true;
            Task.Run(async () =>
            {
                await _splashUseCase.LoadAsync();
                IsLoading = false;
            });
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        bool _isLoading;
    }
}
