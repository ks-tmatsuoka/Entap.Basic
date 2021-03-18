using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Splash
{
    public class SplashPageViewModel : PageViewModelBase
    {
        readonly ISplashPageUseCase _useCase;
        public SplashPageViewModel()
        {
            _useCase = Startup.ServiceProvider.GetService<ISplashPageUseCase>();
            SetPageLifeCycle(_useCase);

            IsLoading = true;
            Task.Run(async () =>
            {
                await _useCase.LoadAsync();
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
