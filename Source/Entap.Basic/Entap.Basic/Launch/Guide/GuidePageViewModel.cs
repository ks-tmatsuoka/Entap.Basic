using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Guide
{
    public class GuidePageViewModel : PageViewModelBase
    {
        IEnumerable<GuideContent> _guideContents;
        IGuideUseCase _guideUseCase;
        public GuidePageViewModel(IEnumerable<GuideContent> contents, IGuideUseCase guideUseCase)
        {
            _guideContents = contents;
            _guideUseCase = guideUseCase;
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
