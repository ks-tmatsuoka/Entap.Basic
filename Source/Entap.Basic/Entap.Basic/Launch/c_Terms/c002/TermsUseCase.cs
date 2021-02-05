using System;
using Entap.Basic.Core;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Terms
{
    public class TermsUseCase : ITermsUseCase
    {
        public TermsUseCase()
        {
        }

        public void Close()
        {
            ProcessManager.Current.Invoke(async () =>
            {
                await PageManager.Navigation.PopModalAsync();
            });
        }
    }
}
