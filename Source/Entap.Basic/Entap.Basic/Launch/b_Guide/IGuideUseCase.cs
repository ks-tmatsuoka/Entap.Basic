using System;

namespace Entap.Basic.Launch.Guide
{
    public interface IGuideUseCase
    {
        void OnNext(int currentPosision);
        void OnBack(int currentPosision);
        void OnComplete();
    }
}
