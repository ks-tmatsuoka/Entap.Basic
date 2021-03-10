using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Guide
{
    public class GuidePageViewModel : PageViewModelBase
    {
        readonly IGuideUseCase _guideUseCase;
        readonly IEnumerable<GuideContent> _guideContents;
        public GuidePageViewModel(IEnumerable<GuideContent> contents)
        {
            _guideUseCase = Startup.ServiceProvider.GetService<IGuideUseCase>();
            SetPageLifeCycle(_guideUseCase);

            _guideContents = contents;
        }

        public IEnumerable<GuideContent> Contents => _guideContents;

        public int Position
        {
            get => _position;
            set
            {
                if (value == _position) return;
                if (value >= Contents.Count()) return;

                if (value > _position)
                    _guideUseCase.OnNext(_position);
                else
                    _guideUseCase.OnBack(_position);

                SetProperty(ref _position, value);
            }
        }
        int _position;

        public Command NextCommand => new Command(() =>
        {
            if (IsLastPosition)
                _guideUseCase.OnComplete();
            else
                Position++;
        });

        bool IsLastPosition => Position == Contents.Count() - 1;
    }
}
