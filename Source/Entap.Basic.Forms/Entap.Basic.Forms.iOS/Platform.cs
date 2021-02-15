using System;
using Xamarin.Forms.Internals;

namespace Entap.Basic.Forms.iOS
{
    [Preserve(AllMembers = true)]
    public static class Platform
    {
        public static void Init()
        {
            // Release構成時、参照できないため、使用するクラスをダミー生成する
            var textContentTypePlatformEffect = Activator.CreateInstance(typeof(PlatformSpecifics.TextContentTypePlatformEffect));
        }
    }
}
