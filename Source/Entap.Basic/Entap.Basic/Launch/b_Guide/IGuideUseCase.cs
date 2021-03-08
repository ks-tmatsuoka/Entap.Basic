using System;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Guide
{
    public interface IGuideUseCase : IPageLifeCycle
    {
        void OnNext(int currentPosision);
        void OnBack(int currentPosision);
        void OnComplete();
    }
}
