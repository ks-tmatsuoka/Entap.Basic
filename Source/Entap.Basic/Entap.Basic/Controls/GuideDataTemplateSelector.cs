using System;
using Entap.Basic.Launch.Guide;
using Xamarin.Forms;

namespace Entap.Basic.Controls
{
    public class GuideDataTemplateSelector : DataTemplateSelector
    {
        public GuideDataTemplateSelector()
        {
        }

        /// <summary>
        /// 画像用DataTemplate
        /// </summary>
        public DataTemplate ImageDataTemplate { get; set; }

        /// <summary>
        /// アニメーション用DataTemplate
        /// </summary>
        public DataTemplate AnimationTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is not GuideContent guideContent) return null;

            return guideContent.ContentType switch
            {
                GuideContentType.Image => ImageDataTemplate,
                GuideContentType.Animation => AnimationTemplate,
                _ => null,
            };
        }
    }
}
