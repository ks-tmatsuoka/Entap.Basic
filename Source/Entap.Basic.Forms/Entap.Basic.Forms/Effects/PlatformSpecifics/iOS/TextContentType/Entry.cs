using System;

namespace Entap.Basic.Forms.PlatformConfiguration.iOSSpecific
{
    using System.Linq;
    using Entap.Basic.Forms.Effects;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using FormsElement = Xamarin.Forms.Entry;
    public static partial class Entry
    {
        static readonly string EffectName = EffectIdManager.GetEffectId("TextContentTypeEffect");

        public static UITextContentType? GetUITextContentType(this IPlatformElementConfiguration<iOS, FormsElement> config)
        {
            return GetTextContentType(config.Element);
        }

        public static IPlatformElementConfiguration<iOS, FormsElement> SetUITextContentType(this IPlatformElementConfiguration<iOS, FormsElement> config, UITextContentType? value)
        {
            SetTextContentType(config.Element, value);
            return config;
        }

        public static readonly BindableProperty TextContentTypeProperty =
            BindableProperty.CreateAttached("TextContentType", typeof(UITextContentType?), typeof(Entry), null, propertyChanged: OnTextContentTypePropertyChanged);

        public static UITextContentType? GetTextContentType(BindableObject view)
        {
            return (UITextContentType?)view.GetValue(TextContentTypeProperty);
        }

        public static void SetTextContentType(BindableObject element, UITextContentType? value)
        {
            element.SetValue(TextContentTypeProperty, value);
        }

        static void OnTextContentTypePropertyChanged(BindableObject element, object oldValue, object newValue)
        {
            if ((UITextContentType?)newValue is null)
                DetachEffect(element as FormsElement);
            else
                AttachEffect(element as FormsElement);
        }

        static void AttachEffect(FormsElement element)
        {
            IElementController controller = element;
            if (controller == null || controller.EffectIsAttached(EffectName))
                return;

            element.Effects.Add(Effect.Resolve(EffectName));
        }

        static void DetachEffect(FormsElement element)
        {
            IElementController controller = element;
            if (controller == null || !controller.EffectIsAttached(EffectName))
                return;

            var toRemove = element.Effects
                .FirstOrDefault(e =>
                    e.ResolveId == Effect.Resolve(EffectName).ResolveId);
            if (toRemove is not null)
                element.Effects.Remove(toRemove);
        }
    }
}
