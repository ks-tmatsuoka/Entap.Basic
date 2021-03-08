using System;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Terms
{
    public interface ITermsUseCase : IPageLifeCycle
    {
        /// <summary>
        /// 閉じる
        /// </summary>
        void Close();
    }
}
