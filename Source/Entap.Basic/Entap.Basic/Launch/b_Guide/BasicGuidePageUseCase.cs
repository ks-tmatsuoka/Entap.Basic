using System;

namespace Entap.Basic.Launch.Guide
{
    public class BasicGuidePageUseCase : IGuidePageUseCase
    {
        public BasicGuidePageUseCase()
        {
        }

        public virtual void OnNext(int currentPosision)
        {
            System.Diagnostics.Debug.WriteLine($"GuideUseCase.OnNext : {currentPosision}");
        }

        public virtual void OnBack(int currentPosision)
        {
            System.Diagnostics.Debug.WriteLine($"GuideUseCase.OnBack : {currentPosision}");
        }

        public virtual void OnComplete()
        {
            System.Diagnostics.Debug.WriteLine("GuideUseCase.OnComplete");
            BasicStartup.PageNavigator.SetTermsPageAsync();
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
