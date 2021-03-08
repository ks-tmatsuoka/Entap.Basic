using System;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Splash
{
    public class SplashPageViewModel : PageViewModelBase
    {
        ISplashUseCase _splashUseCase;
        public SplashPageViewModel(ISplashUseCase splashUseCase)
        {
            _splashUseCase = splashUseCase;
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
