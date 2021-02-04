using System;
namespace Entap.Basic.Launch.Guide
{
    /// <summary>
    /// ガイドコンテンツ
    /// </summary>
    public class GuideContent
    {
        public GuideContent()
        {
        }

        /// <summary>
        /// コンテンツの種類
        /// </summary>
        public GuideContentType ContentType { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// コンテンツソース
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// スライド遷移文字
        /// </summary>
        public string Next { get; set; }
    }
}
