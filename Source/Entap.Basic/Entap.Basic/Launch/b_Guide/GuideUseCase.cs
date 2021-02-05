using System;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Forms;
using Entap.Basic.Launch.Terms;
using Xamarin.Forms;

namespace Entap.Basic.Launch.Guide
{
    public class GuideUseCase : IGuideUseCase
    {
        public GuideUseCase()
        {
        }

        public void OnNext(int currentPosision)
        {
            System.Diagnostics.Debug.WriteLine($"GuideUseCase.OnNext : {currentPosision}");
        }

        public void OnBack(int currentPosision)
        {
            System.Diagnostics.Debug.WriteLine($"GuideUseCase.OnBack : {currentPosision}");
        }

        public void OnComplete()
        {
            System.Diagnostics.Debug.WriteLine("GuideUseCase.OnComplete");
            // ToDo 遷移先ページ変更
            ProcessManager.Current.Invoke(async () => await PageManager.Navigation.SetMainPage<ConfirmTermsPage>(new ConfirmTermsPageViewModel(new ConfirmTermsUseCase())));
        }
    }
}
