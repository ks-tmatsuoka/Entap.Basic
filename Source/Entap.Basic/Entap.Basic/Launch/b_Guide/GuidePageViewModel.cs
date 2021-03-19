using System;
using System.Collections.Generic;
using System.Linq;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Guide
{
    public class GuidePageViewModel : PageViewModelBase
    {
        readonly IGuidePageUseCase _useCase;
        readonly IEnumerable<GuideContent> _guideContents;
        public GuidePageViewModel(IEnumerable<GuideContent> contents)
        {
            _useCase = BasicStartup.GetUseCase<IGuidePageUseCase>();
            SetPageLifeCycle(_useCase);

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
                    _useCase.OnNext(_position);
                else
                    _useCase.OnBack(_position);

                SetProperty(ref _position, value);
            }
        }
        int _position;

        public Command NextCommand => new Command(() =>
        {
            if (IsLastPosition)
                _useCase.OnComplete();
            else
                Position++;
        });

        bool IsLastPosition => Position == Contents.Count() - 1;
    }
}
