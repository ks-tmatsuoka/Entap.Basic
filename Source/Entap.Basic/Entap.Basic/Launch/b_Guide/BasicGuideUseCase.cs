using System;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Terms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Guide
{
    public class BasicGuideUseCase : IGuideUseCase
    {
        public BasicGuideUseCase()
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
            // ToDo 遷移先ページ変更
            ProcessManager.Current.Invoke(async () => await PageManager.Navigation.SetMainPage<ConfirmTermsPage>(new ConfirmTermsPageViewModel()));
        }

        #region IPageLifeCycle
        public virtual void OnCreate() { }

        public virtual void OnDestroy() { }

        public virtual void OnEntry() { }

        public virtual void OnExit() { }
        #endregion
    }
}
