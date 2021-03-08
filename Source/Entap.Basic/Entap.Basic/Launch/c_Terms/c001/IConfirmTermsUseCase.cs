using System;
using Entap.Basic.Forms;

namespace Entap.Basic.Launch.Terms
{
    public interface IConfirmTermsUseCase : IPageLifeCycle
    {
        /// <summary>
        /// 利用規約の確認
        /// </summary>
        void ConfirmTerms();

        /// <summary>
        /// プライバシーポリシーの確認
        /// </summary>
        void ConfirmPrivacyPolicy();

        /// <summary>
        /// 同意チェックボックスの状態変更
        /// </summary>
        void ChangeChecked(bool isChecked);

        /// <summary>
        /// 規約の同意
        /// </summary>
        void AcceptTerms();
    }
}
