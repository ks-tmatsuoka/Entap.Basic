using System;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Guide;

namespace Sample
{
    public class MyGuideUseCase : IGuideUseCase
    {
        public MyGuideUseCase()
        {
        }

        public void OnBack(int currentPosision)
        {
            System.Diagnostics.Debug.WriteLine($"GuideUseCase.OnBack : {currentPosision}");
            
        }

        public virtual void OnNext(int currentPosision)
        {
            System.Diagnostics.Debug.WriteLine($"GuideUseCase.OnNext : {currentPosision}");
            
        }

        public void OnComplete()
        {
            System.Diagnostics.Debug.WriteLine("GuideUseCase.OnComplete");
            ProcessManager.Current.Invoke(async () => await PageManager.Navigation.SetNavigationMainPage<MainPage>(new MainPageViewModel()));
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
